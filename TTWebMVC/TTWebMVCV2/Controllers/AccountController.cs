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
using SNGCommon;
using TTWebApi.Services;
using TTWebCommon.Models;

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

         if (appUser != null || !appUser.Active)
         {
            return RedirectToAction(nameof(Login));
         }
         // create claims
         // auth properties
         // sign in async

         return RedirectToAction("Index", "Home");
      }
   }
}
