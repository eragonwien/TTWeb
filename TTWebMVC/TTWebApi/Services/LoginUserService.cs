using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
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
   public interface ILoginUserService
   {
      Task<LoginUser> Authenticate(string username, string password);
      Task<LoginUser> Reauthenticate(string expiredToken, string refreshToken);
      Task<List<LoginUser>> GetAll();
      Task<LoginUser> GetOne(int id);
      Task<LoginUser> GetOne(string username);
      Task Create(LoginUser user);
      Task Remove(LoginUser user);
      Task<bool> Exist(int id);
      Task<bool> Exist(string username);
   }
   public class LoginUserService : ILoginUserService
   {
      private readonly TTWebDbContext db;
      private readonly AppSettings appSettings;
      private readonly IAuthenticationService authService;

      public LoginUserService(TTWebDbContext db, IOptions<AppSettings> appSettings, IAuthenticationService authService)
      {
         this.db = db;
         this.appSettings = appSettings.Value;
         this.authService = authService;
      }

      public async Task<LoginUser> Authenticate(string username, string password)
      {
         var user = await db.LoginUserSet
            .Include(u => u.LoginUserRolesMapping).ThenInclude(rm => rm.LoginUserRole)
            .FirstOrDefaultAsync(u => u.Email == username && !u.Disabled && authService.IsPasswordValid(password, u.Password));

         if (user == null)
         {
            return null;
         }
         // generates JWT token
         user.AccessToken = CreateAuthToken(user);

         // generates refresh token
         user.RefreshToken = CreateRefreshAuthToken();
         db.Entry(user).Property(u => u.RefreshToken).IsModified = true;
         await db.SaveChangesAsync();

         // clears password
         user.Password = null;

         return user;
      }

      private string CreateRefreshAuthToken()
      {
         return AuthenticationHelper.GenerateRandomToken(appSettings.RefreshAuthTokenLength);
      }

      private string CreateAuthToken(LoginUser loginUser)
      {
         var tokenHandler = new JwtSecurityTokenHandler();
         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.AuthSecret));
         var jwtToken = new JwtSecurityToken(
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(appSettings.AuthTokenDurationMinute),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            claims: AddClaims(loginUser)
         );
         return tokenHandler.WriteToken(jwtToken);
      }

      private IEnumerable<Claim> AddClaims(LoginUser loginUser)
      {
         var claims = new List<Claim>()
         {
            new Claim(ClaimTypes.Email, loginUser.Email),
            new Claim(ClaimTypes.NameIdentifier, loginUser.Id.ToString()),
            new Claim(ClaimTypes.GivenName, loginUser.Firstname),
            new Claim(ClaimTypes.Name, loginUser.Lastname),
            new Claim(ClaimTypes.AuthenticationMethod, AuthenticationMethod.JWT.ToString()),
         };
         foreach (var role in loginUser.LoginUserRolesMapping.Select(r => r.LoginUserRole))
         {
            claims.Add(new Claim(ClaimTypes.Role, role.Name.ToString()));
         }
         return claims;
      }

      private async Task<LoginUser> GetUserFromExpiredToken(string expiredToken)
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

         int loginUserId = Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
         var loginUser = await GetOne(loginUserId);
         return loginUser;
      }

      public async Task Create(LoginUser loginUser)
      {
         db.LoginUserSet.Add(loginUser);
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

      public async Task<List<LoginUser>> GetAll()
      {
         var users = await db.LoginUserSet
            .AsNoTracking()
            .ToListAsync();
         users = users.Select(u =>
         {
            u.Password = null;
            return u;
         }).ToList();
         return users;
      }

      public async Task<LoginUser> GetOne(int id)
      {
         var user = await db.LoginUserSet
            .Include(u => u.LoginUserRolesMapping).ThenInclude(rm => rm.LoginUserRole)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
         if (user != null)
         {
            user.Password = null;
         }
         return user;
      }

      public async Task<LoginUser> GetOne(string username)
      {
         var user = await db.LoginUserSet
            .Include(u => u.LoginUserRolesMapping).ThenInclude(rm => rm.LoginUserRole)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == username);
         user.Password = null;
         return user;
      }

      public async Task Remove(LoginUser user)
      {
         db.Entry(user).State = EntityState.Deleted;
         await db.SaveChangesAsync();
      }

      public async Task<LoginUser> Reauthenticate(string expiredToken, string refreshToken)
      {
         var loginUser = await GetUserFromExpiredToken(expiredToken);
         if (loginUser == null)
         {
            return loginUser;
         }
         loginUser.AccessToken = CreateAuthToken(loginUser);

         loginUser.RefreshToken = CreateRefreshAuthToken();
         db.Entry(loginUser).Property(u => u.RefreshToken).IsModified = true;
         await db.SaveChangesAsync();
         return loginUser;
      }
   }
}
