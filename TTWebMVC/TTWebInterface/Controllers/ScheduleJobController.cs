using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTWebCommon.Facebook;
using TTWebInterface.Models;
namespace TTWebInterface.Controllers
{
   public class ScheduleJobController : Controller
   {
      private readonly TTWebDbContext _context;
      private readonly IFacebookService facebookService;

      public ScheduleJobController(TTWebDbContext context, IFacebookService facebookService)
      {
         _context = context;
         this.facebookService = facebookService;
      }

      // GET: ScheduleJob
      public async Task<IActionResult> Index()
      {
         var tTWebDbContext = _context.ScheduleJobSet.Include(s => s.AppUser).Include(s => s.Type);
         return View(await tTWebDbContext.ToListAsync());
      }

      // GET: ScheduleJob/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var scheduleJob = await _context.ScheduleJobSet
             .Include(s => s.AppUser)
             .Include(s => s.Type)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (scheduleJob == null)
         {
            return NotFound();
         }

         return View(scheduleJob);
      }

      // GET: ScheduleJob/Create
      public IActionResult Create()
      {
         ViewData["AppUserId"] = new SelectList(_context.AppUserSet, "Id", "Email");
         ViewData["ScheduleJobTypeId"] = new SelectList(_context.ScheduleJobTypeSet, "Id", "Name");
         return View();
      }

      // POST: ScheduleJob/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,Name,AppUserId,ScheduleJobTypeId")] ScheduleJob scheduleJob)
      {
         if (ModelState.IsValid)
         {
            _context.Add(scheduleJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         ViewData["AppUserId"] = new SelectList(_context.AppUserSet, "Id", "Email", scheduleJob.AppUserId);
         ViewData["ScheduleJobTypeId"] = new SelectList(_context.ScheduleJobTypeSet, "Id", "Name", scheduleJob.ScheduleJobTypeId);
         return View(scheduleJob);
      }

      // GET: ScheduleJob/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var scheduleJob = await _context.ScheduleJobSet.FindAsync(id);
         if (scheduleJob == null)
         {
            return NotFound();
         }
         ViewData["AppUserId"] = new SelectList(_context.AppUserSet, "Id", "Email", scheduleJob.AppUserId);
         ViewData["ScheduleJobTypeId"] = new SelectList(_context.ScheduleJobTypeSet, "Id", "Name", scheduleJob.ScheduleJobTypeId);
         return View(scheduleJob);
      }

      // POST: ScheduleJob/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AppUserId,ScheduleJobTypeId")] ScheduleJob scheduleJob)
      {
         if (id != scheduleJob.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(scheduleJob);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!ScheduleJobExists(scheduleJob.Id))
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
         ViewData["AppUserId"] = new SelectList(_context.AppUserSet, "Id", "Email", scheduleJob.AppUserId);
         ViewData["ScheduleJobTypeId"] = new SelectList(_context.ScheduleJobTypeSet, "Id", "Name", scheduleJob.ScheduleJobTypeId);
         return View(scheduleJob);
      }

      // GET: ScheduleJob/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var scheduleJob = await _context.ScheduleJobSet
             .Include(s => s.AppUser)
             .Include(s => s.Type)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (scheduleJob == null)
         {
            return NotFound();
         }

         return View(scheduleJob);
      }

      // POST: ScheduleJob/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var scheduleJob = await _context.ScheduleJobSet.FindAsync(id);
         _context.ScheduleJobSet.Remove(scheduleJob);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      public async Task<IActionResult> Test(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }
         var scheduleJob = await _context.ScheduleJobSet
             .Include(s => s.AppUser)
             .Include(s => s.Type)
             .Include(s => s.Parameters).ThenInclude(p => p.Type)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (scheduleJob == null)
         {
            return NotFound();
         }
         facebookService.Execute(scheduleJob.ToFacebookParameters());
         return RedirectToAction(nameof(Index));
      }

      private bool ScheduleJobExists(int id)
      {
         return _context.ScheduleJobSet.Any(e => e.Id == id);
      }
   }
}
