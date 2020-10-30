using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TTWebMVCV2.Models;

namespace TTWebMVCV2.Controllers
{
   public class ErrorController : BaseController
   {
      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Index()
      {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }
   }
}
