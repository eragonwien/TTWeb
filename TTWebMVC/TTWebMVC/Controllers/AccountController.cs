using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNGCommon.Common;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TTWebMVC.Controllers
{
   [AllowAnonymous]
   public class AccountController : Controller
   {
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

         // TODO: Retrieves user from database

         // Sign-in 
         var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, authResult.Principal.FindFirstValue(ClaimTypes.Email)));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Surname, authResult.Principal.FindFirstValue(ClaimTypes.Surname)));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, authResult.Principal.FindFirstValue(ClaimTypes.GivenName)));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessToken, authResult.Properties.GetTokenValue(AuthenticationSettings.TokenAccessToken)));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessTokenExpiredAt, authResult.Properties.GetTokenValue(AuthenticationSettings.TokenExpiredAt)));

         var authProperties = new AuthenticationProperties
         {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.Now.AddHours(1),
            IsPersistent = true
         };

         await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
         await HttpContext.SignOutAsync(AuthenticationSettings.SchemeExternal);

         return RedirectToAction("index", "home");
      }
   }
}