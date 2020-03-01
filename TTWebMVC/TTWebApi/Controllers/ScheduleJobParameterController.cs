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
   public class ScheduleJobParameterController : ControllerBase
   {
      private readonly TTWebDbContext _context;

      public ScheduleJobParameterController(TTWebDbContext context)
      {
         _context = context;
      }

      // GET: api/ScheduleJobParameter
      [HttpGet]
      public async Task<ActionResult<IEnumerable<ScheduleJobParameter>>> GetScheduleJobParameterSet()
      {
         return await _context.ScheduleJobParameterSet.ToListAsync();
      }

      // GET: api/ScheduleJobParameter/5
      [HttpGet("{id}")]
      public async Task<ActionResult<ScheduleJobParameter>> GetScheduleJobParameter(int id)
      {
         var scheduleJobParameter = await _context.ScheduleJobParameterSet.FindAsync(id);

         if (scheduleJobParameter == null)
         {
            return NotFound();
         }

         return scheduleJobParameter;
      }

      // PUT: api/ScheduleJobParameter/5
      [HttpPut("{id}")]
      public async Task<IActionResult> PutScheduleJobParameter(int id, ScheduleJobParameter scheduleJobParameter)
      {
         if (id != scheduleJobParameter.Id)
         {
            return BadRequest();
         }

         _context.Entry(scheduleJobParameter).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!ScheduleJobParameterExists(id))
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

      // POST: api/ScheduleJobParameter
      [HttpPost]
      public async Task<ActionResult<ScheduleJobParameter>> PostScheduleJobParameter(ScheduleJobParameter scheduleJobParameter)
      {
         _context.ScheduleJobParameterSet.Add(scheduleJobParameter);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetScheduleJobParameter", new { id = scheduleJobParameter.Id }, scheduleJobParameter);
      }

      // DELETE: api/ScheduleJobParameter/5
      [HttpDelete("{id}")]
      public async Task<ActionResult<ScheduleJobParameter>> DeleteScheduleJobParameter(int id)
      {
         var scheduleJobParameter = await _context.ScheduleJobParameterSet.FindAsync(id);
         if (scheduleJobParameter == null)
         {
            return NotFound();
         }

         _context.ScheduleJobParameterSet.Remove(scheduleJobParameter);
         await _context.SaveChangesAsync();

         return scheduleJobParameter;
      }

      private bool ScheduleJobParameterExists(int id)
      {
         return _context.ScheduleJobParameterSet.Any(e => e.Id == id);
      }
   }
}
