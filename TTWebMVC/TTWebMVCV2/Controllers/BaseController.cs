using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Interactions;
using SNGCommon;
using TTWebApi.Services;
using TTWebCommon.Models;

namespace TTWebMVCV2.Controllers
{
   public class BaseController : Controller
   {
      protected IActionResult RedirectToActionNoQueryString(string actionName, string controllerName)
      {
         RouteData.Values.Clear();
         return base.RedirectToAction(actionName, controllerName);
      }

      protected int UserId
      {
         get
         {
            if (User.Identity.IsAuthenticated)
            {
               return int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int parsedId) ? parsedId : 0;
            }
            return 0;
         }
      }
   }
}
