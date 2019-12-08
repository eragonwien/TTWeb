using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SNGCommon.Common;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TTWebMVC.Models.Facebook;

namespace TTWebMVC.Models
{
   public abstract class BaseUser
   {
      public long Id { get; set; } = 0;
      public string Email { get; set; }
      public string Firstname { get; set; }
      public string Lastname { get; set; }
      public string FacebookId { get; set; }
   }

   public class AppUser : BaseUser
   {
      public string AccessToken { get; set; }
      public DateTimeOffset? AccessTokenExpirationDate { get; set; }
      public List<ScheduleJob> ScheduleJobs { get; set; } = new List<ScheduleJob>();

      public static AppUser FromAuthentication(AuthenticateResult authResult)
      {
         return new AppUser
         {
            Email = authResult.Principal.FindFirstValue(ClaimTypes.Email),
            Firstname = authResult.Principal.FindFirstValue(ClaimTypes.GivenName),
            Lastname = authResult.Principal.FindFirstValue(ClaimTypes.Surname),
            FacebookId = authResult.Principal.FindFirstValue(ClaimTypes.NameIdentifier),
            AccessToken = authResult.Properties.GetTokenValue(AuthenticationSettings.TokenAccessToken),
            AccessTokenExpirationDate = DateTime.TryParse(authResult.Properties.GetTokenValue(AuthenticationSettings.TokenExpiredAt), out DateTime tempExpirationDate) ? (DateTime?)tempExpirationDate : null
         };
      }

      public void UpdateToken(FacebookTokenInfo tokenResult)
      {
         AccessToken = tokenResult.AccessToken;
         AccessTokenExpirationDate = tokenResult.ExpirationDate;
      }

      public static AppUser FromUser(ClaimsPrincipal user)
      {
         return new AppUser
         {
            AccessToken = user.FindFirstValue(AuthenticationSettings.ClaimTypeAccessToken),
            AccessTokenExpirationDate = DateTimeOffset.Parse(user.FindFirstValue(AuthenticationSettings.ClaimTypeAccessTokenExpiredAt)),
            FacebookId = user.FindFirstValue(AuthenticationSettings.ClaimTypeFacebookId),
            Email = user.FindFirstValue(ClaimTypes.Email),
            Firstname = user.FindFirstValue(ClaimTypes.GivenName),
            Lastname = user.FindFirstValue(ClaimTypes.Surname),
            Id = Convert.ToInt64(user.FindFirstValue(ClaimTypes.NameIdentifier))
         };
      }

      public ClaimsIdentity BuildClaimIdentity()
      {
         var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, Email));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Surname, Lastname));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, Firstname));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Id.ToString()));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeFacebookId, FacebookId));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessToken, AccessToken));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessTokenExpiredAt, AccessTokenExpirationDate.ToString()));
         return claimsIdentity;
      }
   }
}
