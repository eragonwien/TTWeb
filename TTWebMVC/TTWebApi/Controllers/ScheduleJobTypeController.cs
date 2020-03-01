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
   public class ScheduleJobTypeController : ControllerBase
   {
      private readonly TTWebDbContext _context;

      public ScheduleJobTypeController(TTWebDbContext context)
      {
         _context = context;
      }

      // GET: api/ScheduleJobType
      [HttpGet]
      public async Task<ActionResult<IEnumerable<ScheduleJobType>>> GetScheduleJobTypeSet()
      {
         return await _context.ScheduleJobTypeSet.ToListAsync();
      }

      // GET: api/ScheduleJobType/5
      [HttpGet("{id}")]
      public async Task<ActionResult<ScheduleJobType>> GetScheduleJobType(int id)
      {
         var scheduleJobType = await _context.ScheduleJobTypeSet.FindAsync(id);

         if (scheduleJobType == null)
         {
            return NotFound();
         }

         return scheduleJobType;
      }

      // PUT: api/ScheduleJobType/5
      [HttpPut("{id}")]
      public async Task<IActionResult> PutScheduleJobType(int id, ScheduleJobType scheduleJobType)
      {
         if (id != scheduleJobType.Id)
         {
            return BadRequest();
         }

         _context.Entry(scheduleJobType).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!ScheduleJobTypeExists(id))
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

      // POST: api/ScheduleJobType
      [HttpPost]
      public async Task<ActionResult<ScheduleJobType>> PostScheduleJobType(ScheduleJobType scheduleJobType)
      {
         _context.ScheduleJobTypeSet.Add(scheduleJobType);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetScheduleJobType", new { id = scheduleJobType.Id }, scheduleJobType);
      }

      // DELETE: api/ScheduleJobType/5
      [HttpDelete("{id}")]
      public async Task<ActionResult<ScheduleJobType>> DeleteScheduleJobType(int id)
      {
         var scheduleJobType = await _context.ScheduleJobTypeSet.FindAsync(id);
         if (scheduleJobType == null)
         {
            return NotFound();
         }

         _context.ScheduleJobTypeSet.Remove(scheduleJobType);
         await _context.SaveChangesAsync();

         return scheduleJobType;
      }

      private bool ScheduleJobTypeExists(int id)
      {
         return _context.ScheduleJobTypeSet.Any(e => e.Id == id);
      }
   }
}
