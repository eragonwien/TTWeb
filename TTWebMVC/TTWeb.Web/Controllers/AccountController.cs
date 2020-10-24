using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly ILoginUserService _loginUserService;
        private readonly AuthenticationAppSetting _authConfig;

        public AccountController(
            ILoginUserService loginUserService,
            AuthenticationAppSetting authenticationAppSetting)
        {
            _loginUserService = loginUserService;
            _authConfig = authenticationAppSetting;
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
            await HttpContext.SignOutAsync(_authConfig.ExternalCookieScheme);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
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
            var authResult = await HttpContext.AuthenticateAsync(_authConfig.ExternalCookieScheme);

            // Try retrive user from database
            string email = authResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _loginUserService.GetUserByEmailAsync(email);

            // If user does not exists, creates an inactive user
            user ??= await _loginUserService.CreateUserAsync(new LoginUserModel
            {
                Email = email,
                FirstName = authResult.Principal.FindFirst(ClaimTypes.GivenName)?.Value,
                LastName = authResult.Principal.FindFirst(ClaimTypes.Surname)?.Value
            });

            // Logouts from external login
            await HttpContext.SignOutAsync(_authConfig.ExternalCookieScheme);

            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = CreateUserClaims(user);
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = CreateSignInAuthenticationProperties();

            // signs in
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);
            return RedirectPermanent("/");
        }

        private IEnumerable<Claim> CreateUserClaims(LoginUserModel user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            yield return new Claim(ClaimTypes.Email, user.Email);
            yield return new Claim(ClaimTypes.GivenName, user.FirstName);
            yield return new Claim(ClaimTypes.Surname, user.LastName);

            foreach (var userPermission in user.UserPermissions)
                yield return new Claim(ClaimTypes.Role, userPermission.ToString());
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
