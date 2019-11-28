using Microsoft.AspNetCore.Mvc;

namespace TTWebMVC.Controllers
{
   public class DefaultController : Controller
   {
      public IActionResult RedirectToHome()
      {
         return RedirectToAction("Index", "Home");
      }
   }
}