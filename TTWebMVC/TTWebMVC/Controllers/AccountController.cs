using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SNGCommon.Common;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TTWebMVC.Models;
using TTWebMVC.Services;

namespace TTWebMVC.Controllers
{
   [AllowAnonymous]
   public class AccountController : DefaultController
   {
      private readonly IUserRepository userRepository;
      private readonly IFacebookClient facebookClient;
      private readonly ILogger<AccountController> log;

      public AccountController(IUserRepository userRepository, IFacebookClient facebookClient, ILogger<AccountController> log)
      {
         this.userRepository = userRepository;
         this.facebookClient = facebookClient;
         this.log = log;
      }

      public IActionResult Login()
      {
         return View();
      }

      public IActionResult LoginExternal()
      {
         var authProperties = new AuthenticationProperties { RedirectUri = Url.Action(nameof(LoginCallback)) };
         return Challenge(authProperties, FacebookDefaults.AuthenticationScheme);
      }

      public async Task<IActionResult> LoginCallback()
      {
         // Authenticate external
         var authResult = await HttpContext.AuthenticateAsync(AuthenticationSettings.SchemeExternal);

         if (!authResult.Succeeded)
         {
            return RedirectToAction(nameof(Login));
         }

         AppUser appUser = new AppUser
         {
            Email = authResult.Principal.FindFirstValue(ClaimTypes.Email),
            Firstname = authResult.Principal.FindFirstValue(ClaimTypes.GivenName),
            Lastname = authResult.Principal.FindFirstValue(ClaimTypes.Surname),
            FacebookId = authResult.Principal.FindFirstValue(ClaimTypes.NameIdentifier),
            AccessToken = authResult.Properties.GetTokenValue(AuthenticationSettings.TokenAccessToken),
            AccessTokenExpirationDate = DateTime.TryParse(authResult.Properties.GetTokenValue(AuthenticationSettings.TokenExpiredAt), out DateTime tempExpirationDate) ? (DateTime?)tempExpirationDate : null
         };

         // TODO: Get longlived access token
         string accessToken = facebookClient.GetLongLivedAccessToken(appUser.AccessToken);

         // Retrieves user from database
         if (!await userRepository.Exists(appUser.Email))
         {
            await userRepository.Create(appUser);
         }
         appUser = await userRepository.GetUser(appUser.Email);

         // Sign-in 
         var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, appUser.Email));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Surname, appUser.Firstname));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, appUser.Lastname));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeFacebookId, appUser.FacebookId));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessToken, appUser.AccessToken));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessTokenExpiredAt, appUser.AccessTokenExpirationDate.ToString()));

         var authProperties = new AuthenticationProperties
         {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.Now.AddHours(1),
            IsPersistent = true
         };

         await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
         await HttpContext.SignOutAsync(AuthenticationSettings.SchemeExternal);

         return RedirectToHome();
      }

      public async Task<IActionResult> Logout()
      {
         await HttpContext.SignOutAsync();
         return RedirectToHome();
      }
   }
}