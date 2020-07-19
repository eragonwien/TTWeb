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
      public PartialViewResult Create()
      {
         return PartialView("~/Views/Schedule/_ScheduleModalPartial.cshtml", new ScheduleDefModalViewModel());
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
            }
            catch (Exception ex)
            {
               AddErrorNotification(ex.Message);
            }
         }
         return RedirectToAction("Index");
      }

      [HttpGet]
      public async Task<IActionResult> Update(int id)
      {
         return PartialView("~/Views/Schedule/_ScheduleModalPartial.cshtml", 
            new ScheduleDefModalViewModel(await scheduleJobService.GetScheduleJobDef(id, UserId)));
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
            }
            catch (Exception ex)
            {
               AddErrorNotification(ex.Message);
            }
         }
         return RedirectToAction("Index");
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task ToggleActive(int id, bool active)
      {
         await scheduleJobService.ToggleActive(id, active);
      }
   }
}
