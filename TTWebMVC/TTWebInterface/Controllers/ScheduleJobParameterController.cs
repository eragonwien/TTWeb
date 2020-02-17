using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TTWebNetTest.Controllers
{
    public class ScheduleJobParameterController : Controller
    {
        private readonly TTWebDbContext _context;

        public ScheduleJobParameterController(TTWebDbContext context)
        {
            _context = context;
        }

        // GET: ScheduleJobParameter
        public async Task<IActionResult> Index()
        {
            var tTWebDbContext = _context.ScheduleJobParameterSet.Include(s => s.Job).Include(s => s.Type);
            return View(await tTWebDbContext.ToListAsync());
        }

        // GET: ScheduleJobParameter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleJobParameter = await _context.ScheduleJobParameterSet
                .Include(s => s.Job)
                .Include(s => s.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduleJobParameter == null)
            {
                return NotFound();
            }

            return View(scheduleJobParameter);
        }

        // GET: ScheduleJobParameter/Create
        public IActionResult Create()
        {
            ViewData["ScheduleJobId"] = new SelectList(_context.ScheduleJobSet, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.ScheduleJobParameterTypeSet, "Id", "Name");
            return View();
        }

        // POST: ScheduleJobParameter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,ScheduleJobId,TypeId")] ScheduleJobParameter scheduleJobParameter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduleJobParameter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleJobId"] = new SelectList(_context.ScheduleJobSet, "Id", "Name", scheduleJobParameter.ScheduleJobId);
            ViewData["TypeId"] = new SelectList(_context.ScheduleJobParameterTypeSet, "Id", "Name", scheduleJobParameter.TypeId);
            return View(scheduleJobParameter);
        }

        // GET: ScheduleJobParameter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleJobParameter = await _context.ScheduleJobParameterSet.FindAsync(id);
            if (scheduleJobParameter == null)
            {
                return NotFound();
            }
            ViewData["ScheduleJobId"] = new SelectList(_context.ScheduleJobSet, "Id", "Name", scheduleJobParameter.ScheduleJobId);
            ViewData["TypeId"] = new SelectList(_context.ScheduleJobParameterTypeSet, "Id", "Name", scheduleJobParameter.TypeId);
            return View(scheduleJobParameter);
        }

        // POST: ScheduleJobParameter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,ScheduleJobId,TypeId")] ScheduleJobParameter scheduleJobParameter)
        {
            if (id != scheduleJobParameter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduleJobParameter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleJobParameterExists(scheduleJobParameter.Id))
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
            ViewData["ScheduleJobId"] = new SelectList(_context.ScheduleJobSet, "Id", "Name", scheduleJobParameter.ScheduleJobId);
            ViewData["TypeId"] = new SelectList(_context.ScheduleJobParameterTypeSet, "Id", "Name", scheduleJobParameter.TypeId);
            return View(scheduleJobParameter);
        }

        // GET: ScheduleJobParameter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleJobParameter = await _context.ScheduleJobParameterSet
                .Include(s => s.Job)
                .Include(s => s.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduleJobParameter == null)
            {
                return NotFound();
            }

            return View(scheduleJobParameter);
        }

        // POST: ScheduleJobParameter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduleJobParameter = await _context.ScheduleJobParameterSet.FindAsync(id);
            _context.ScheduleJobParameterSet.Remove(scheduleJobParameter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleJobParameterExists(int id)
        {
            return _context.ScheduleJobParameterSet.Any(e => e.Id == id);
        }
    }
}
