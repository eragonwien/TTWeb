using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TTWebApi.Services;
using TTWebMVCV2.Models;

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
