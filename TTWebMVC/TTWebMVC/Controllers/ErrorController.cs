using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TTWebMVC.Models;

namespace TTWebMVC.Controllers
{
   public class ErrorController : Controller
   {
      private readonly ILogger<ErrorController> log;

      public ErrorController(ILogger<ErrorController> log)
      {
         this.log = log;
      }

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Index()
      {
         var ex = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
         var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
         log.LogCritical("[Unhandled Exception] RequestId={0}, MSG={1}, Stack={2}", requestId, ex.Error.Message, ex.Error.StackTrace);
         return View(new ErrorViewModel { RequestId = requestId });
      }

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult PageNotFound()
      {
         return View();
      }
   }
}