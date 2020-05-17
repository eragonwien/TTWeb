using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenQA.Selenium;
using SNGCommon.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TTWebApi.Models;
using TTWebCommon.Models;
using TTWebCommon.Services;

namespace TTWebApi.Services
{
   public interface IAppUserService
   {
      Task<AppUser> Authenticate(string username, string password);
      Task<AppUser> Reauthenticate(string expiredToken, string refreshToken);
      Task<List<AppUser>> GetAll();
      Task<AppUser> GetOne(int id);
      Task<AppUser> GetOne(string username);
      Task Create(AppUser user);
      Task Remove(AppUser user);
      Task<bool> Exist(int id);
      Task<bool> Exist(string username);
      Task UpdateProfile(AppUser appUser);
   }
   public class AppUserService : IAppUserService
   {
      private readonly TTWebDbContext db;
      private readonly AppSettings appSettings;
      private readonly IAuthenticationService authService;

      public AppUserService(TTWebDbContext db, IOptions<AppSettings> appSettings, IAuthenticationService authService)
      {
         this.db = db;
         this.appSettings = appSettings.Value;
         this.authService = authService;
      }

      public async Task<AppUser> Authenticate(string username, string password)
      {
         var user = await db.AppUserSet
            .Include(u => u.AppUserRoles).ThenInclude(rm => rm.Role)
            .FirstOrDefaultAsync(u => u.Email == username && !u.Disabled && authService.IsPasswordValid(password, u.Password));

         if (user == null)
         {
            return null;
         }

         user = InjectAuthToken(user);

         db.Entry(user).Property(u => u.RefreshToken).IsModified = true;
         await db.SaveChangesAsync();

         return user;
      }

      private AppUser InjectAuthToken(AppUser appUser)
      {
         appUser.AccessToken = CreateJwtAuthToken(appUser);
         appUser.RefreshToken = CreateRefreshAuthToken();
         return appUser;
      }

      private string CreateRefreshAuthToken()
      {
         return AuthenticationHelper.GenerateRandomToken(appSettings.RefreshAuthTokenLength);
      }

      private string CreateJwtAuthToken(AppUser appUser)
      {
         var tokenHandler = new JwtSecurityTokenHandler();
         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.AuthSecret));
         var jwtToken = new JwtSecurityToken(
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(appSettings.AuthTokenDurationMinute),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            claims: GetClaims(appUser)
         );
         return tokenHandler.WriteToken(jwtToken);
      }

      private IEnumerable<Claim> GetClaims(AppUser appUser)
      {
         var claims = new List<Claim>()
         {
            new Claim(ClaimTypes.Email, appUser.Email),
            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new Claim(ClaimTypes.GivenName, appUser.Firstname),
            new Claim(ClaimTypes.Name, appUser.Lastname),
            new Claim(ClaimTypes.AuthenticationMethod, AuthenticationMethod.JWT.ToString()),
         };
         foreach (var role in appUser.AppUserRoles.Select(r => r.Role))
         {
            claims.Add(new Claim(ClaimTypes.Role, role.Name.ToString()));
         }
         return claims;
      }

      private async Task<AppUser> GetUserFromExpiredToken(string expiredToken)
      {
         var tokenHandler = new JwtSecurityTokenHandler();
         var tokenValidationParameters = new TokenValidationParameters
         {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.AuthSecret))
         };
         var principal = tokenHandler.ValidateToken(expiredToken, tokenValidationParameters, out SecurityToken securityToken);
         if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
         {
            throw new SecurityTokenException("Invalid Token");
         }

         int appUserId = Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
         var appUser = await GetOne(appUserId);
         return appUser;
      }

      public async Task Create(AppUser appUser)
      {
         db.AppUserSet.Add(appUser);
         await db.SaveChangesAsync();
      }

      public async Task<bool> Exist(int id)
      {
         return await GetOne(id) != null;
      }

      public async Task<bool> Exist(string username)
      {
         return await GetOne(username) != null;
      }

      public async Task<List<AppUser>> GetAll()
      {
         var users = await db.AppUserSet
            .AsNoTracking()
            .ToListAsync();
         users = users.Select(u =>
         {
            u.Password = null;
            return u;
         }).ToList();
         return users;
      }

      public async Task<AppUser> GetOne(int id)
      {
         var user = await db.AppUserSet
            .Include(u => u.AppUserRoles).ThenInclude(rm => rm.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
         if (user != null)
         {
            user.Password = null;
         }
         return user;
      }

      public async Task<AppUser> GetOne(string username)
      {
         var user = await db.AppUserSet
            .Include(u => u.AppUserRoles).ThenInclude(rm => rm.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == username);
         user.Password = null;
         return user;
      }

      public async Task Remove(AppUser user)
      {
         db.Entry(user).State = EntityState.Deleted;
         await db.SaveChangesAsync();
      }

      public async Task<AppUser> Reauthenticate(string expiredToken, string refreshToken)
      {
         var appUser = await GetUserFromExpiredToken(expiredToken);
         if (appUser == null)
         {
            return appUser;
         }
         appUser.AccessToken = CreateJwtAuthToken(appUser);

         appUser.RefreshToken = CreateRefreshAuthToken();
         db.Entry(appUser).Property(u => u.RefreshToken).IsModified = true;
         await db.SaveChangesAsync();
         return appUser;
      }

      public Task UpdateProfile(AppUser appUser)
      {
         db.Entry(appUser).Property(u => u.Title).IsModified = true;
         db.Entry(appUser).Property(u => u.Firstname).IsModified = true;
         db.Entry(appUser).Property(u => u.Lastname).IsModified = true;
         db.Entry(appUser).Property(u => u.Email).IsModified = true;
         return db.SaveChangesAsync();
      }
   }
}
