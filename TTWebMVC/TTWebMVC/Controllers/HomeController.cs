using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTWebMVC.Models;

namespace TTWebMVC.Controllers
{
   [Authorize]
   public class HomeController : DefaultController
   {
      public IActionResult Index()
      {
         return View();
      }

      [AllowAnonymous]
      public IActionResult Privacy()
      {
         return View();
      }
   }
}
