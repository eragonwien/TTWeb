using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTWebCommon.Models;

namespace TTWebInterface.Controllers
{
   public class ScheduleJobTypeController : Controller
   {
      private readonly TTWebDbContext _context;

      public ScheduleJobTypeController(TTWebDbContext context)
      {
         _context = context;
      }

      // GET: ScheduleJobType
      public async Task<IActionResult> Index()
      {
         return View(await _context.ScheduleJobTypeSet.ToListAsync());
      }

      // GET: ScheduleJobType/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var scheduleJobType = await _context.ScheduleJobTypeSet
             .FirstOrDefaultAsync(m => m.Id == id);
         if (scheduleJobType == null)
         {
            return NotFound();
         }

         return View(scheduleJobType);
      }

      // GET: ScheduleJobType/Create
      public IActionResult Create()
      {
         return View();
      }

      // POST: ScheduleJobType/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,Name")] ScheduleJobType scheduleJobType)
      {
         if (ModelState.IsValid)
         {
            _context.Add(scheduleJobType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         return View(scheduleJobType);
      }

      // GET: ScheduleJobType/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var scheduleJobType = await _context.ScheduleJobTypeSet.FindAsync(id);
         if (scheduleJobType == null)
         {
            return NotFound();
         }
         return View(scheduleJobType);
      }

      // POST: ScheduleJobType/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ScheduleJobType scheduleJobType)
      {
         if (id != scheduleJobType.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(scheduleJobType);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!ScheduleJobTypeExists(scheduleJobType.Id))
               {
                  return NotFound();
               }
               else
               {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         return View(scheduleJobType);
      }

      // GET: ScheduleJobType/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var scheduleJobType = await _context.ScheduleJobTypeSet
             .FirstOrDefaultAsync(m => m.Id == id);
         if (scheduleJobType == null)
         {
            return NotFound();
         }

         return View(scheduleJobType);
      }

      // POST: ScheduleJobType/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var scheduleJobType = await _context.ScheduleJobTypeSet.FindAsync(id);
         _context.ScheduleJobTypeSet.Remove(scheduleJobType);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool ScheduleJobTypeExists(int id)
      {
         return _context.ScheduleJobTypeSet.Any(e => e.Id == id);
      }
   }
}
