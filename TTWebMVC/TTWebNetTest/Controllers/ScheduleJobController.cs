using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTWebNetCommon.Models;

namespace TTWebNetTest.Controllers
{
   public class ScheduleJobController : BaseController
   {
      // GET: ScheduleJob
      public async Task<ActionResult> Index()
      {
         var scheduleJobSet = db.ScheduleJobSet.Include(s => s.AppUser).Include(s => s.Type);
         return View(await scheduleJobSet.ToListAsync());
      }

      // GET: ScheduleJob/Details/5
      public async Task<ActionResult> Details(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         ScheduleJob scheduleJob = await db.ScheduleJobSet.FindAsync(id);
         if (scheduleJob == null)
         {
            return HttpNotFound();
         }
         return View(scheduleJob);
      }

      // GET: ScheduleJob/Create
      public ActionResult Create()
      {
         ViewBag.AppUserId = new SelectList(db.AppUserSet, "Id", "Email");
         ViewBag.ScheduleJobTypeId = new SelectList(db.ScheduleJobTypeSet, "Id", "Name");
         return View();
      }

      // POST: ScheduleJob/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Create([Bind(Include = "Id,Name,AppUserId,ScheduleJobTypeId")] ScheduleJob scheduleJob)
      {
         if (ModelState.IsValid)
         {
            db.ScheduleJobSet.Add(scheduleJob);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
         }

         ViewBag.AppUserId = new SelectList(db.AppUserSet, "Id", "Email", scheduleJob.AppUserId);
         ViewBag.ScheduleJobTypeId = new SelectList(db.ScheduleJobTypeSet, "Id", "Name", scheduleJob.ScheduleJobTypeId);
         return View(scheduleJob);
      }

      // GET: ScheduleJob/Edit/5
      public async Task<ActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         ScheduleJob scheduleJob = await db.ScheduleJobSet.FindAsync(id);
         if (scheduleJob == null)
         {
            return HttpNotFound();
         }
         ViewBag.AppUserId = new SelectList(db.AppUserSet, "Id", "Email", scheduleJob.AppUserId);
         ViewBag.ScheduleJobTypeId = new SelectList(db.ScheduleJobTypeSet, "Id", "Name", scheduleJob.ScheduleJobTypeId);
         return View(scheduleJob);
      }

      // POST: ScheduleJob/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Edit([Bind(Include = "Id,Name,AppUserId,ScheduleJobTypeId")] ScheduleJob scheduleJob)
      {
         if (ModelState.IsValid)
         {
            db.Entry(scheduleJob).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
         }
         ViewBag.AppUserId = new SelectList(db.AppUserSet, "Id", "Email", scheduleJob.AppUserId);
         ViewBag.ScheduleJobTypeId = new SelectList(db.ScheduleJobTypeSet, "Id", "Name", scheduleJob.ScheduleJobTypeId);
         return View(scheduleJob);
      }

      // GET: ScheduleJob/Delete/5
      public async Task<ActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         ScheduleJob scheduleJob = await db.ScheduleJobSet.FindAsync(id);
         if (scheduleJob == null)
         {
            return HttpNotFound();
         }
         return View(scheduleJob);
      }

      // POST: ScheduleJob/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> DeleteConfirmed(int id)
      {
         ScheduleJob scheduleJob = await db.ScheduleJobSet.FindAsync(id);
         db.ScheduleJobSet.Remove(scheduleJob);
         await db.SaveChangesAsync();
         return RedirectToAction("Index");
      }

      public async Task<ActionResult> Test(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         ScheduleJob scheduleJob = await db.ScheduleJobSet
            .Include(s => s.AppUser)
            .Include(s => s.Type)
            .Include(s => s.Parameters)
            .SingleOrDefaultAsync(s => s.Id == id);
         if (scheduleJob == null)
         {
            return HttpNotFound();
         }

         var result = fb.Execute(scheduleJob.ToFacebookParameters());
         return View(result);
      }
   }
}
