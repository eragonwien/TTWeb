using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            var authResult = await HttpContext.AuthenticateAsync(AuthenticationSettings.SchemeExternal);

            // Get & create user
            string email = authResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var appUser = await appUserService.GetUserByEmailAsync(email);
            if (appUser == null)
            {
                await appUserService.CreateUserAsync(new AppUser
                {
                    Email = email,
                    Firstname = authResult.Principal.FindFirst(ClaimTypes.GivenName)?.Value,
                    Lastname = authResult.Principal.FindFirst(ClaimTypes.Surname)?.Value
                });
                appUser = await appUserService.GetUserByEmailAsync(email);
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
            var model = new ProfileUserViewModel(await appUserService.GetUserByIdAsync(UserId));
            UserFacebookCredentials ??= await appUserService.GetFacebookCredentialsByUserIdAsync(UserId);
            model.FacebookCredentials = UserFacebookCredentials;
            UserFacebookFriends ??= await appUserService.GetFacebookFriendsByUserIdAsync(UserId);
            model.FacebookFriends = UserFacebookFriends;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Profile(ProfileUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await appUserService.UpdateUserProfileAsync(model.ToAppUser());
            }
        }

        [HttpGet]
        public async Task<IActionResult> FacebookCredentials()
        {
            return View(UserFacebookCredentials ?? await appUserService.GetFacebookCredentialsByUserIdAsync(UserId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFacebookCredential(FacebookCredentialViewModel model)
        {
            if (ModelState.IsValid)
            {
                await appUserService.TryUpdateFacebookPasswordAsync(UserId, model.Id.GetValueOrDefault(0), model.Username, model.Password);
                UserFacebookCredentials = await appUserService.GetFacebookCredentialsByUserIdAsync(UserId);
                return PartialView("~/Views/Account/_FacebookCredentialsListPartial.cshtml", UserFacebookCredentials);
            }
            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task DeleteFacebookCredential(string username)
        {
            await appUserService.DeleteFacebookCredentialAsync(username, UserId);
            UserFacebookCredentials.RemoveAll(c => c.Username == username);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFacebookFriend(FacebookFriendViewModel model)
        {
            if (ModelState.IsValid)
            {
                await appUserService.TryUpdateFacebookFriendByUserIdAsync(UserId, model.Id.GetValueOrDefault(0), model.Name, model.ProfileLink);
                UserFacebookFriends = await appUserService.GetFacebookFriendsByUserIdAsync(UserId);
                return PartialView("~/Views/Account/_FacebookFriendsListPartial.cshtml", UserFacebookFriends);
            }
            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task DeleteFacebookFriend(string id)
        {
            await appUserService.DeleteFacebookFriendByUserIdAsync(id, UserId);
            UserFacebookFriends.RemoveAll(f => f.Id.ToString() == id);
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
