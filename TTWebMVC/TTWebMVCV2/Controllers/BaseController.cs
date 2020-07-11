using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Interactions;
using SNGCommon;
using TTWebCommon.Models;

namespace TTWebMVCV2.Controllers
{
   public class BaseController : Controller
   {
      public const string TempDataErrorNotificationsKey = "Error.TempMessage";
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

      protected void AddErrorNotification(string text)
      {
         var notifications = (List<string>)TempData[TempDataErrorNotificationsKey] ?? new List<string>();
         if (!notifications.Contains(text))
         {
            notifications.Add(text);
         }
         TempData[TempDataErrorNotificationsKey] = notifications;
      }
   }
}
