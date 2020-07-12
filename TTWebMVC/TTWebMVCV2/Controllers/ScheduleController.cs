using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SNGCommon;
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

      public ScheduleController(ILogger<ScheduleController> log, IScheduleJobService scheduleJobService)
      {
         this.log = log;
         this.scheduleJobService = scheduleJobService;
      }

      [HttpGet]
      public async Task<IActionResult> Index()
      {
         ViewBag.ScheduleTypeList = Helper.GetEnumStrings<ScheduleJobType>(true).Select(s => s.ToStringCapitalized());
         ViewBag.IntervalTypeList = Helper.GetEnumStrings<IntervalTypeEnum>(true).Select(s => s.ToStringCapitalized());

         TimeZoneInfo timeZoneInfo = TimeZoneInfo.Utc;
         ViewBag.TimeZoneSelectList = TimeZoneInfo.GetSystemTimeZones().Select(tz => new SelectListItem(tz.DisplayName, tz.Id, tz.Id == TimeZoneInfo.Utc.Id));
         return View(await scheduleJobService.GetScheduleJobDefs(UserId));
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(CreateScheduleViewModel model)
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
   }
}
