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
    public class ScheduleJobTypeController : BaseController
    {
        // GET: ScheduleJobType
        public async Task<ActionResult> Index()
        {
            return View(await db.ScheduleJobTypeSet.ToListAsync());
        }

        // GET: ScheduleJobType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleJobType scheduleJobType = await db.ScheduleJobTypeSet.FindAsync(id);
            if (scheduleJobType == null)
            {
                return HttpNotFound();
            }
            return View(scheduleJobType);
        }

        // GET: ScheduleJobType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ScheduleJobType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] ScheduleJobType scheduleJobType)
        {
            if (ModelState.IsValid)
            {
                db.ScheduleJobTypeSet.Add(scheduleJobType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(scheduleJobType);
        }

        // GET: ScheduleJobType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleJobType scheduleJobType = await db.ScheduleJobTypeSet.FindAsync(id);
            if (scheduleJobType == null)
            {
                return HttpNotFound();
            }
            return View(scheduleJobType);
        }

        // POST: ScheduleJobType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] ScheduleJobType scheduleJobType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduleJobType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(scheduleJobType);
        }

        // GET: ScheduleJobType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleJobType scheduleJobType = await db.ScheduleJobTypeSet.FindAsync(id);
            if (scheduleJobType == null)
            {
                return HttpNotFound();
            }
            return View(scheduleJobType);
        }

        // POST: ScheduleJobType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ScheduleJobType scheduleJobType = await db.ScheduleJobTypeSet.FindAsync(id);
            db.ScheduleJobTypeSet.Remove(scheduleJobType);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
