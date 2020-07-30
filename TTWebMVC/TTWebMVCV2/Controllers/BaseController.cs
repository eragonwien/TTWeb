using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenQA.Selenium.Interactions;
using SNGCommon;
using TTWebCommon.Models;

namespace TTWebMVCV2.Controllers
{
    public class BaseController : Controller
    {
        public const string TempDataErrorNotificationsKey = "Error.TempMessage";
        public const string TempDataSuccessNotificationsKey = "Success.TempMessage";
        protected IActionResult RedirectToActionNoQueryString(string actionName, string controllerName)
        {
            RouteData.Values.Clear();
            return base.RedirectToAction(actionName, controllerName);
        }

        #region Getters

        private const string SessionKeyUserFacebookCredentials = "SessionKey.UserFacebookCredentials";
        private const string SessionKeyUserFacebookFriends = "SessionKey.UserFacebookFriends";
        private const string SessionKeyUserTimezone = "SessionKey.UserTimezone";

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

        protected List<FacebookCredential> UserFacebookCredentials
        {
            get
            {
                return ReadSession<List<FacebookCredential>>(SessionKeyUserFacebookCredentials);
            }
            set
            {
                WriteToSession(SessionKeyUserFacebookCredentials, value);
            }
        }

        protected List<FacebookFriend> UserFacebookFriends
        {
            get
            {
                return ReadSession<List<FacebookFriend>>(SessionKeyUserFacebookFriends);
            }
            set
            {
                WriteToSession(SessionKeyUserFacebookFriends, value);
            }
        }

        protected string UserTimezone
        {
            get
            {
                return HttpContext.Session.GetString(SessionKeyUserTimezone);
            }
            set
            {
                HttpContext.Session.SetString(SessionKeyUserTimezone, value);
            }
        }

        #endregion

        protected void AddErrorNotification(string text)
        {
            var notifications = (List<string>)TempData[TempDataErrorNotificationsKey] ?? new List<string>();
            if (!notifications.Contains(text))
            {
                notifications.Add(text);
            }
            TempData[TempDataErrorNotificationsKey] = notifications;
        }

        protected void AddSuccessNotification(string text)
        {
            var notifications = (List<string>)TempData[TempDataSuccessNotificationsKey] ?? new List<string>();
            if (!notifications.Contains(text))
            {
                notifications.Add(text);
            }
            TempData[TempDataSuccessNotificationsKey] = notifications;
        }

        #region Session

        private void WriteToSession<T>(string key, T value)
        {
            HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value));
        }

        private T ReadSession<T>(string key)
        {
            string strValue = HttpContext.Session.GetString(key);
            return !string.IsNullOrWhiteSpace(strValue) ? JsonConvert.DeserializeObject<T>(strValue) : default;
        }

        #endregion
    }
}
