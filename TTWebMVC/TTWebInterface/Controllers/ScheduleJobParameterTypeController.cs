using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTWebInterface.Models;

namespace TTWebInterface.Controllers
{
    public class ScheduleJobParameterTypeController : Controller
    {
        private readonly TTWebDbContext _context;

        public ScheduleJobParameterTypeController(TTWebDbContext context)
        {
            _context = context;
        }

        // GET: ScheduleJobParameterType
        public async Task<IActionResult> Index()
        {
            return View(await _context.ScheduleJobParameterTypeSet.ToListAsync());
        }

        // GET: ScheduleJobParameterType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleJobParameterType = await _context.ScheduleJobParameterTypeSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduleJobParameterType == null)
            {
                return NotFound();
            }

            return View(scheduleJobParameterType);
        }

        // GET: ScheduleJobParameterType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScheduleJobParameterType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ScheduleJobParameterType scheduleJobParameterType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduleJobParameterType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scheduleJobParameterType);
        }

        // GET: ScheduleJobParameterType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleJobParameterType = await _context.ScheduleJobParameterTypeSet.FindAsync(id);
            if (scheduleJobParameterType == null)
            {
                return NotFound();
            }
            return View(scheduleJobParameterType);
        }

        // POST: ScheduleJobParameterType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ScheduleJobParameterType scheduleJobParameterType)
        {
            if (id != scheduleJobParameterType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduleJobParameterType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleJobParameterTypeExists(scheduleJobParameterType.Id))
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
            return View(scheduleJobParameterType);
        }

        // GET: ScheduleJobParameterType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleJobParameterType = await _context.ScheduleJobParameterTypeSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduleJobParameterType == null)
            {
                return NotFound();
            }

            return View(scheduleJobParameterType);
        }

        // POST: ScheduleJobParameterType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduleJobParameterType = await _context.ScheduleJobParameterTypeSet.FindAsync(id);
            _context.ScheduleJobParameterTypeSet.Remove(scheduleJobParameterType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleJobParameterTypeExists(int id)
        {
            return _context.ScheduleJobParameterTypeSet.Any(e => e.Id == id);
        }
    }
}
