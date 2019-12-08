using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTWebMVC.Models;
using TTWebMVC.Models.ViewModels;
using TTWebMVC.Services;

namespace TTWebMVC.Controllers
{
   [Authorize]
   public class ScheduleController : DefaultController
   {
      private readonly IJobService jobService;
      private readonly ILogger<ScheduleController> log;

      public ScheduleController(IJobService jobService, ILogger<ScheduleController> log)
      {
         this.jobService = jobService;
         this.log = log;
      }

      // GET: Job
      public ActionResult Index()
      {
         return View();
      }

      // GET: Job/Details/5
      public ActionResult Details(int id)
      {
         return View();
      }

      // GET: Job/Create
      public async Task<IActionResult> Create()
      {
         var model = new JobViewModel
         {
            Types = (await jobService.GetAllTypes()).Select(t => new SelectListItem(t, t)).ToList(),
            ParameterTypes = (await jobService.GetAllParameterTypes()).Select(t => new SelectListItem(t, t)).ToList(),
            Parameters = new List<ScheduleJobParameter> { new ScheduleJobParameter { Type = ScheduleJobParameterType.TEXT } }
         };
         return View(model);
      }

      // POST: Job/Create
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(JobViewModel model)
      {
         try
         {
            await jobService.Add(ScheduleJob.FromJobViewModel(model).AddAppUser(AppUser.FromUser(User)));
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            log.LogError("Fehler bei der Erstellung vom Job: {0}", ex.Message);
            return View(model);
         }
         
      }

      // GET: Job/Edit/5
      public ActionResult Edit(int id)
      {
         return View();
      }

      // POST: Job/Edit/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit(int id, IFormCollection collection)
      {
         try
         {
            // TODO: Add update logic here

            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View();
         }
      }

      // GET: Job/Delete/5
      public ActionResult Delete(int id)
      {
         return View();
      }

      // POST: Job/Delete/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete(int id, IFormCollection collection)
      {
         try
         {
            // TODO: Add delete logic here

            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View();
         }
      }
   }
}