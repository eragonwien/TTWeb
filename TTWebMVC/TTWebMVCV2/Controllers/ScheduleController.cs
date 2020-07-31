using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SNGCommon;
using SNGCommon.Extenstions.ArrayExtensions;
using SNGCommon.Extenstions.StringExtensions;
using TimeZoneConverter;
using TTWebCommon.Models;
using TTWebCommon.Services;
using TTWebMVCV2.Models;

namespace TTWebMVCV2.Controllers
{
    public class ScheduleController : BaseController
    {
        private readonly ILogger<ScheduleController> log;
        private readonly IScheduleJobService scheduleJobService;
        private readonly IAppUserService appUserService;

        public ScheduleController(ILogger<ScheduleController> log, IScheduleJobService scheduleJobService, IAppUserService appUserService)
        {
            this.log = log;
            this.scheduleJobService = scheduleJobService;
            this.appUserService = appUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.FriendsList = await appUserService.FacebookFriends(UserId);
            return View((await scheduleJobService.GetScheduleJobDefs(UserId)).GroupIntoBundles(3));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await BuildScheduleDefViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScheduleDefViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await scheduleJobService.AddScheduleJobDef(model.ToScheduleJobDef(UserId));
                    SetSuccessNotification("Schedule added successfully");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(await BindSelectLists(model));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateModel = await BuildScheduleDefViewModel(id);
            return View(updateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ScheduleDefViewModel model)
        {
            if (ModelState.IsValid && model.Id > 0)
            {
                try
                {
                    await scheduleJobService.UpdateScheduleJobDef(model.ToScheduleJobDef(UserId));
                    SetSuccessNotification("Schedule {0} updated successfully", model.Id);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(await BindSelectLists(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task ToggleActive(int id, bool active)
        {
            await scheduleJobService.ToggleActive(id, active);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await scheduleJobService.RemoveScheduleJobDef(id, UserId);
            SetSuccessNotification("Schedule {0} successfully removed", id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckTimezone(string timezone)
        {
            if (string.IsNullOrWhiteSpace(UserTimezone))
            {
                UserTimezone = TZConvert.IanaToWindows(timezone);
            }
            return Ok(UserTimezone);
        }

        private async Task<ScheduleDefViewModel> BuildScheduleDefViewModel(int id = 0)
        {
            var model = id > 0 ? new ScheduleDefViewModel(await scheduleJobService.GetScheduleJobDef(id, UserId)) : ScheduleDefViewModel.Default;
            await BindSelectLists(model);
            return model;
        }

        private async Task<ScheduleDefViewModel> BindSelectLists(ScheduleDefViewModel model)
        {
            model.SetTimezoneSelectList(UserTimezone)
               .SetLogins(UserFacebookCredentials ?? await appUserService.FacebookCredentials(UserId))
               .SetFriends(UserFacebookFriends ?? await appUserService.FacebookFriends(UserId));

            return model;
        }
    }
}
