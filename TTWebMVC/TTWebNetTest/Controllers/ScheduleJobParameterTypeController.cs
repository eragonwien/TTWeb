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
   public class ScheduleJobParameterTypeController : BaseController
   {
      // GET: ScheduleJobParameterType
      public async Task<ActionResult> Index()
      {
         return View(await db.ScheduleJobParameterTypeSet.ToListAsync());
      }

      // GET: ScheduleJobParameterType/Details/5
      public async Task<ActionResult> Details(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         ScheduleJobParameterType scheduleJobParameterType = await db.ScheduleJobParameterTypeSet.FindAsync(id);
         if (scheduleJobParameterType == null)
         {
            return HttpNotFound();
         }
         return View(scheduleJobParameterType);
      }

      // GET: ScheduleJobParameterType/Create
      public ActionResult Create()
      {
         return View();
      }

      // POST: ScheduleJobParameterType/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Create([Bind(Include = "Id,Name")] ScheduleJobParameterType scheduleJobParameterType)
      {
         if (ModelState.IsValid)
         {
            db.ScheduleJobParameterTypeSet.Add(scheduleJobParameterType);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
         }

         return View(scheduleJobParameterType);
      }

      // GET: ScheduleJobParameterType/Edit/5
      public async Task<ActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         ScheduleJobParameterType scheduleJobParameterType = await db.ScheduleJobParameterTypeSet.FindAsync(id);
         if (scheduleJobParameterType == null)
         {
            return HttpNotFound();
         }
         return View(scheduleJobParameterType);
      }

      // POST: ScheduleJobParameterType/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] ScheduleJobParameterType scheduleJobParameterType)
      {
         if (ModelState.IsValid)
         {
            db.Entry(scheduleJobParameterType).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
         }
         return View(scheduleJobParameterType);
      }

      // GET: ScheduleJobParameterType/Delete/5
      public async Task<ActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         ScheduleJobParameterType scheduleJobParameterType = await db.ScheduleJobParameterTypeSet.FindAsync(id);
         if (scheduleJobParameterType == null)
         {
            return HttpNotFound();
         }
         return View(scheduleJobParameterType);
      }

      // POST: ScheduleJobParameterType/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> DeleteConfirmed(int id)
      {
         ScheduleJobParameterType scheduleJobParameterType = await db.ScheduleJobParameterTypeSet.FindAsync(id);
         db.ScheduleJobParameterTypeSet.Remove(scheduleJobParameterType);
         await db.SaveChangesAsync();
         return RedirectToAction("Index");
      }
   }
}
