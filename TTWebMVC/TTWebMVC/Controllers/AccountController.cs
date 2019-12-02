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

         AppUser appUser = AppUser.FromAuthentication(authResult);

         // Gets longlived access token
         var tokenResult = await facebookClient.GetLongLivedAccessToken(appUser.AccessToken);
         appUser.UpdateToken(tokenResult);

         // Retrieves user from database
         if (!await userRepository.Exists(appUser.Email))
         {
            await userRepository.Create(appUser);
         }
         else
         {
            await userRepository.Update(appUser);
         }
         appUser = await userRepository.GetUser(appUser.Email);

         // Sign-in 
         ClaimsIdentity claimsIdentity = appUser.BuildClaimIdentity();
         AuthenticationProperties authProperties = new AuthenticationProperties
         {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.Now.AddHours(1),
            IsPersistent = true
         };

         await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
         await HttpContext.SignOutAsync(AuthenticationSettings.SchemeExternal);

         return RedirectToHome();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Logout()
      {
         await HttpContext.SignOutAsync();
         return RedirectToHome();
      }
   }
}