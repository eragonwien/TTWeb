using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Configurations;
using TTWeb.BusinessLogic.Models;
using TTWeb.BusinessLogic.Services;

namespace TTWeb.Web.Controllers
{
    public class AccountController : TTWebBaseController
    {
        private readonly ILoginUserService loginUserService;
        private readonly AuthenticationAppSetting authenticationAppSetting;

        public AccountController(
            ILoginUserService loginUserService,
            AuthenticationAppSetting authenticationAppSetting)
        {
            this.loginUserService = loginUserService;
            this.authenticationAppSetting = authenticationAppSetting;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> LoginAsync()
        {
            await LogoutAsync();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(authenticationAppSetting.ExternalCookieScheme);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return Ok(Url.Action(nameof(LoginAsync)));
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
            var authResult = await HttpContext.AuthenticateAsync(authenticationAppSetting.ExternalCookieScheme);

            // Get & create user
            string email = authResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var user = await loginUserService.GetUserByEmailAsync(email);
            if (user == null)
            {
                await loginUserService.CreateUserAsync(new LoginUserModel
                {
                    Email = email,
                    FirstName = authResult.Principal.FindFirst(ClaimTypes.GivenName)?.Value,
                    LastName = authResult.Principal.FindFirst(ClaimTypes.Surname)?.Value
                });
                user = await loginUserService.GetUserByEmailAsync(email);
            }

            // logouts from external login
            await HttpContext.SignOutAsync(authenticationAppSetting.ExternalCookieScheme);

            if (user == null)
                throw new Exception("Login user should not be null here");

            // create claims
            var claims = CreateUserClaims(user);
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = CreateSignInAuthenticationProperties();

            // signIn
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);
            return RedirectPermanent("/");
        }

        private AuthenticationProperties CreateSignInAuthenticationProperties()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Claim> CreateUserClaims(LoginUserModel appUser)
        {
            throw new NotImplementedException();
        }
    }
}
