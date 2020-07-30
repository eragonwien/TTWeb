using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;
using TTWebCommon.Services;
using TTWebMVCV2.Controllers;

namespace TTWebMVCV2.Models
{
    public abstract class BaseView<TModel> : RazorPage<TModel>
    {
        private const string viewDataScheduleButtonEnabled = "ScheduleButtonEnabled";

        [RazorInject]
        public IAppUserService AppUserService { get; set; }
        public bool Authenticated
        {
            get
            {
                return User.Identity.IsAuthenticated;
            }
        }

        public AppUser ContextUser
        {
            get
            {
                return AppUserService.LoadContextUser(User);
            }
        }

        public bool ScheduleButtonEnabled
        {
            get
            {
                if (ViewData[viewDataScheduleButtonEnabled] == null)
                {
                    return false;
                }
                return (bool)ViewData[viewDataScheduleButtonEnabled];
            }
            set
            {
                ViewData[viewDataScheduleButtonEnabled] = value;
            }
        }

        public string[] ErrorNotifications
        {
            get
            {
                return (string[])TempData[BaseController.TempDataErrorNotificationsKey] ?? new string[0];
            }
        }

        public string[] SuccessNotifications
        {
            get
            {
                return (string[])TempData[BaseController.TempDataSuccessNotificationsKey] ?? new string[0];
            }
        }
    }
}
