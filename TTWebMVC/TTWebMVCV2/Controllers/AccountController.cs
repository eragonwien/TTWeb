using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Interactions;
using SNGCommon;
using TTWebCommon.Models;
using TTWebCommon.Services;
using TTWebMVCV2.Models;

namespace TTWebMVCV2.Controllers
{
   public class AccountController : BaseController
   {
      private readonly ILogger<AccountController> log;
      private readonly IAppUserService appUserService;

      public AccountController(ILogger<AccountController> log, IAppUserService appUserService)
      {
         this.log = log;
         this.appUserService = appUserService;
      }

      [AllowAnonymous]
      [HttpGet]
      public async Task<IActionResult> Login()
      {
         await Logout();
         return View();
      }

      [AllowAnonymous]
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Logout()
      {
         await HttpContext.SignOutAsync(AuthenticationSettings.SchemeExternal);
         await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
         return Ok(Url.Action(nameof(Login)));
      }

      [AllowAnonymous]
      [HttpGet]
      public IActionResult LoginExternal(string provider)
      {
         var authProps = new AuthenticationProperties()
         {
            RedirectUri = Url.Action(nameof(LoginExternalCallback)),
            AllowRefresh = true,
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.Now.AddDays(7)
         };
         return Challenge(authProps, provider);
      }

      [AllowAnonymous]
      [HttpGet]
      public async Task<IActionResult> LoginExternalCallback()
      {
         var authResult = await HttpContext.AuthenticateAsync(AuthenticationSettings.SchemeExternal);

         // Get & create user
         string email = authResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
         var appUser = await appUserService.GetOne(email);
         if (appUser == null)
         {
            await appUserService.Create(new AppUser
            {
               Email = email,
               Firstname = authResult.Principal.FindFirst(ClaimTypes.GivenName)?.Value,
               Lastname = authResult.Principal.FindFirst(ClaimTypes.Surname)?.Value
            });
            appUser = await appUserService.GetOne(email);
         }

         // logouts from external login
         await HttpContext.SignOutAsync(AuthenticationSettings.SchemeExternal);

         if (appUser == null)
         {
            return RedirectToAction(nameof(Login));
         }

         if (!appUser.Active)
         {
            return RedirectToActionNoQueryString("NotActivated", "Account");
         }

         if (appUser.Disabled)
         {
            return RedirectToActionNoQueryString("Disabled", "Account");
         }

         // create claims
         var claims = appUserService.CreateUserClaims(appUser);
         var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
         var authProperties = CreateSignInAuthenticationProperties();

         // signIn
         await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);
         return RedirectPermanent("/");
      }

      [AllowAnonymous]
      [HttpGet]
      public IActionResult NotActivated(AppUser appUser)
      {
         return View(appUser);
      }

      [AllowAnonymous]
      [HttpGet]
      public IActionResult Disabled(AppUser appUser)
      {
         return View(appUser);
      }

      [HttpGet]
      public async Task<IActionResult> Profile()
      {
         var model = new ProfileUserViewModel(await appUserService.GetOne(UserId));
         model.FacebookCredentials = await appUserService.FacebookCredentials(UserId);
         model.FacebookFriends = await appUserService.FacebookFriends(UserId);
         return View(model);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task Profile(ProfileUserViewModel model)
      {
         if (ModelState.IsValid)
         {
            await appUserService.UpdateProfile(model.ToAppUser());
         }
      }

      [HttpGet]
      public async Task<IActionResult> FacebookCredentials()
      {
         return View(await appUserService.FacebookCredentials(UserId));
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> UpdateFacebookCredential(FacebookCredentialViewModel model)
      {
         if (ModelState.IsValid)
         {
            await appUserService.TryUpdateFacebookPassword(UserId, model.Id.GetValueOrDefault(0), model.Username, model.Password);
            return PartialView("~/Views/Account/_FacebookCredentialsListPartial.cshtml", await appUserService.FacebookCredentials(UserId));
         }
         return NoContent();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task DeleteFacebookCredential(string username)
      {
         await appUserService.DeleteFacebookCredential(username, UserId);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> UpdateFacebookFriend(FacebookFriendViewModel model)
      {
         if (ModelState.IsValid)
         {
            await appUserService.TryUpdateFacebookFriend(UserId, model.Id.GetValueOrDefault(0), model.Name, model.ProfileLink);
            return PartialView("~/Views/Account/_FacebookFriendsListPartial.cshtml", await appUserService.FacebookFriends(UserId));
         }
         return NoContent();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task DeleteFacebookFriend(string id)
      {
         await appUserService.DeleteFacebookFriend(id, UserId);
      }

      private AuthenticationProperties CreateSignInAuthenticationProperties()
      {
         return new AuthenticationProperties
         {
            AllowRefresh = true,
            IsPersistent = true,
            IssuedUtc = DateTimeOffset.UtcNow
         };
      }
   }
}
