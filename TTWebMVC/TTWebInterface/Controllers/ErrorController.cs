using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TTWebInterface.Models;

namespace TTWebInterface.Controllers
{
   [AllowAnonymous]
   public class ErrorController : Controller
   {
      public IActionResult Index()
      {
         var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
         return View(new ErrorViewModel 
         { 
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            Exception = exceptionHandlerPathFeature?.Error,
            ShowException = exceptionHandlerPathFeature != null
         });
      }
   }
}