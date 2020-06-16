using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
   public interface IAccountService
   {
      Task<AppUser> Authenticate(string username, string password);
      Task<AppUser> Reauthenticate(string expiredToken, string refreshToken);
   }
   public class AccountService : IAccountService
   {
      private readonly TTWebDbContext db;
      private readonly AppSettings appSettings;
      private readonly IAuthenticationService authService;
      private readonly IAppUserService appUserService;

      public AccountService(TTWebDbContext db, IOptions<AppSettings> appSettings, IAuthenticationService authService, IAppUserService appUserService)
      {
         this.db = db;
         this.appSettings = appSettings.Value;
         this.authService = authService;
         this.appUserService = appUserService;
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
            new Claim(ClaimTypes.Surname, appUser.Lastname),
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
         var appUser = await appUserService.GetOne(appUserId);
         return appUser;
      }
   }
}
