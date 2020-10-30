using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TTWebMVCV2.Controllers
{
   public class HomeController : BaseController
   {
      private readonly ILogger<HomeController> log;

      public HomeController(ILogger<HomeController> log)
      {
         this.log = log;
      }

      public IActionResult Index()
      {
         return View();
      }

      public IActionResult Privacy()
      {
         return View();
      }
   }
}
