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
   public class ScheduleJobParameterController : BaseController
   {
      // GET: ScheduleJobParameter
      public async Task<ActionResult> Index()
      {
         var scheduleJobParameterSet = db.ScheduleJobParameterSet.Include(s => s.Job).Include(s => s.Type);
         return View(await scheduleJobParameterSet.ToListAsync());
      }

      // GET: ScheduleJobParameter/Details/5
      public async Task<ActionResult> Details(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         ScheduleJobParameter scheduleJobParameter = await db.ScheduleJobParameterSet.FindAsync(id);
         if (scheduleJobParameter == null)
         {
            return HttpNotFound();
         }
         return View(scheduleJobParameter);
      }

      // GET: ScheduleJobParameter/Create
      public ActionResult Create()
      {
         ViewBag.ScheduleJobId = new SelectList(db.ScheduleJobSet, "Id", "Name");
         ViewBag.TypeId = new SelectList(db.ScheduleJobParameterTypeSet, "Id", "Name");
         return View();
      }

      // POST: ScheduleJobParameter/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Create([Bind(Include = "Id,Value,ScheduleJobId,TypeId")] ScheduleJobParameter scheduleJobParameter)
      {
         if (ModelState.IsValid)
         {
            db.ScheduleJobParameterSet.Add(scheduleJobParameter);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
         }

         ViewBag.ScheduleJobId = new SelectList(db.ScheduleJobSet, "Id", "Name", scheduleJobParameter.ScheduleJobId);
         ViewBag.TypeId = new SelectList(db.ScheduleJobParameterTypeSet, "Id", "Name", scheduleJobParameter.TypeId);
         return View(scheduleJobParameter);
      }

      // GET: ScheduleJobParameter/Edit/5
      public async Task<ActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         ScheduleJobParameter scheduleJobParameter = await db.ScheduleJobParameterSet.FindAsync(id);
         if (scheduleJobParameter == null)
         {
            return HttpNotFound();
         }
         ViewBag.ScheduleJobId = new SelectList(db.ScheduleJobSet, "Id", "Name", scheduleJobParameter.ScheduleJobId);
         ViewBag.TypeId = new SelectList(db.ScheduleJobParameterTypeSet, "Id", "Name", scheduleJobParameter.TypeId);
         return View(scheduleJobParameter);
      }

      // POST: ScheduleJobParameter/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Edit([Bind(Include = "Id,Value,ScheduleJobId,TypeId")] ScheduleJobParameter scheduleJobParameter)
      {
         if (ModelState.IsValid)
         {
            db.Entry(scheduleJobParameter).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
         }
         ViewBag.ScheduleJobId = new SelectList(db.ScheduleJobSet, "Id", "Name", scheduleJobParameter.ScheduleJobId);
         ViewBag.TypeId = new SelectList(db.ScheduleJobParameterTypeSet, "Id", "Name", scheduleJobParameter.TypeId);
         return View(scheduleJobParameter);
      }

      // GET: ScheduleJobParameter/Delete/5
      public async Task<ActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         ScheduleJobParameter scheduleJobParameter = await db.ScheduleJobParameterSet.FindAsync(id);
         if (scheduleJobParameter == null)
         {
            return HttpNotFound();
         }
         return View(scheduleJobParameter);
      }

      // POST: ScheduleJobParameter/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> DeleteConfirmed(int id)
      {
         ScheduleJobParameter scheduleJobParameter = await db.ScheduleJobParameterSet.FindAsync(id);
         db.ScheduleJobParameterSet.Remove(scheduleJobParameter);
         await db.SaveChangesAsync();
         return RedirectToAction("Index");
      }
   }
}
