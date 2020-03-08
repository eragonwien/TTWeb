using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenQA.Selenium;
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
      Task SaveChanges();
   }
   public class LoginUserService : ILoginUserService
   {
      private readonly TTWebDbContext db;
      private readonly AppSettings appSettings;

      public LoginUserService(TTWebDbContext db, IOptions<AppSettings> appSettings)
      {
         this.db = db;
         this.appSettings = appSettings.Value;
      }

      public async Task<LoginUser> Authenticate(string username, string password)
      {
         var user = await db.LoginUserSet
            .Include(u => u.LoginUserRole)
            .FirstOrDefaultAsync(u => u.Email == username);
         if (user == null)
         {
            return null;
         }
         // generates JWT token
         user.Token = CreateAuthToken(user);

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

      private string CreateAuthToken(LoginUser user)
      {
         var tokenHandler = new JwtSecurityTokenHandler();
         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.AuthSecret));
         var jwtToken = new JwtSecurityToken(
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(appSettings.AuthTokenDurationDay),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            claims: new Claim[]
            {
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.GivenName, user.Firstname),
               new Claim(ClaimTypes.Name, user.Lastname),
               new Claim(ClaimTypes.AuthenticationMethod, AuthenticationMethod.JWT.ToString()),
               new Claim(ClaimTypes.Role, string.Join(";",user.LoginUserRole.Name)),
            }
         );
         return tokenHandler.WriteToken(jwtToken);
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

         int userId = Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier));
         var loginUser = await GetOne(userId);
         return loginUser;
      }

      public async Task Create(LoginUser user)
      {
         db.LoginUserSet.Add(user);
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
         var user = await db.LoginUserSet.FindAsync(id);
         user.Password = null;
         return user;
      }

      public async Task<LoginUser> GetOne(string username)
      {
         var user = await db.LoginUserSet.FirstOrDefaultAsync(u => u.Email == username);
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
         loginUser.Token = CreateAuthToken(loginUser);
         loginUser.RefreshToken = CreateRefreshAuthToken();
         db.Entry(loginUser).Property(u => u.Token).IsModified = true;
         db.Entry(loginUser).Property(u => u.RefreshToken).IsModified = true;
         await db.SaveChangesAsync();
         return loginUser;
      }

      public Task SaveChanges()
      {
         return db.SaveChangesAsync();
      }
   }
}
