using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TTWebMVC.Models.Common;

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
         var authResult = await HttpContext.AuthenticateAsync(AuthenticationSettings.SchemeExternal);

         return RedirectToAction("index", "home");
      }
   }
}