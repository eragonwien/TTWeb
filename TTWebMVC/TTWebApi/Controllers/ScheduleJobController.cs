using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTWebCommon.Models;

namespace TTWebApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ScheduleJobController : ControllerBase
   {
      private readonly TTWebDbContext _context;

      public ScheduleJobController(TTWebDbContext context)
      {
         _context = context;
      }

      // GET: api/ScheduleJob
      [HttpGet]
      public async Task<ActionResult<IEnumerable<ScheduleJob>>> GetScheduleJobSet()
      {
         return await _context.ScheduleJobSet.ToListAsync();
      }

      // GET: api/ScheduleJob/5
      [HttpGet("{id}")]
      public async Task<ActionResult<ScheduleJob>> GetScheduleJob(int id)
      {
         var scheduleJob = await _context.ScheduleJobSet.FindAsync(id);

         if (scheduleJob == null)
         {
            return NotFound();
         }

         return scheduleJob;
      }

      // PUT: api/ScheduleJob/5
      [HttpPut("{id}")]
      public async Task<IActionResult> PutScheduleJob(int id, ScheduleJob scheduleJob)
      {
         if (id != scheduleJob.Id)
         {
            return BadRequest();
         }

         _context.Entry(scheduleJob).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!ScheduleJobExists(id))
            {
               return NotFound();
            }
            else
            {
               throw;
            }
         }

         return NoContent();
      }

      // POST: api/ScheduleJob
      [HttpPost]
      public async Task<ActionResult<ScheduleJob>> PostScheduleJob(ScheduleJob scheduleJob)
      {
         _context.ScheduleJobSet.Add(scheduleJob);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetScheduleJob", new { id = scheduleJob.Id }, scheduleJob);
      }

      // DELETE: api/ScheduleJob/5
      [HttpDelete("{id}")]
      public async Task<ActionResult<ScheduleJob>> DeleteScheduleJob(int id)
      {
         var scheduleJob = await _context.ScheduleJobSet.FindAsync(id);
         if (scheduleJob == null)
         {
            return NotFound();
         }

         _context.ScheduleJobSet.Remove(scheduleJob);
         await _context.SaveChangesAsync();

         return scheduleJob;
      }

      private bool ScheduleJobExists(int id)
      {
         return _context.ScheduleJobSet.Any(e => e.Id == id);
      }
   }
}
