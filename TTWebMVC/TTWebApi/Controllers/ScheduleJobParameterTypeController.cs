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
   public class ScheduleJobParameterTypeController : ControllerBase
   {
      private readonly TTWebDbContext _context;

      public ScheduleJobParameterTypeController(TTWebDbContext context)
      {
         _context = context;
      }

      // GET: api/ScheduleJobParameterType
      [HttpGet]
      public async Task<ActionResult<IEnumerable<ScheduleJobParameterType>>> GetScheduleJobParameterTypeSet()
      {
         return await _context.ScheduleJobParameterTypeSet.ToListAsync();
      }

      // GET: api/ScheduleJobParameterType/5
      [HttpGet("{id}")]
      public async Task<ActionResult<ScheduleJobParameterType>> GetScheduleJobParameterType(int id)
      {
         var scheduleJobParameterType = await _context.ScheduleJobParameterTypeSet.FindAsync(id);

         if (scheduleJobParameterType == null)
         {
            return NotFound();
         }

         return scheduleJobParameterType;
      }

      // PUT: api/ScheduleJobParameterType/5
      [HttpPut("{id}")]
      public async Task<IActionResult> PutScheduleJobParameterType(int id, ScheduleJobParameterType scheduleJobParameterType)
      {
         if (id != scheduleJobParameterType.Id)
         {
            return BadRequest();
         }

         _context.Entry(scheduleJobParameterType).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!ScheduleJobParameterTypeExists(id))
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

      // POST: api/ScheduleJobParameterType
      [HttpPost]
      public async Task<ActionResult<ScheduleJobParameterType>> PostScheduleJobParameterType(ScheduleJobParameterType scheduleJobParameterType)
      {
         _context.ScheduleJobParameterTypeSet.Add(scheduleJobParameterType);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetScheduleJobParameterType", new { id = scheduleJobParameterType.Id }, scheduleJobParameterType);
      }

      // DELETE: api/ScheduleJobParameterType/5
      [HttpDelete("{id}")]
      public async Task<ActionResult<ScheduleJobParameterType>> DeleteScheduleJobParameterType(int id)
      {
         var scheduleJobParameterType = await _context.ScheduleJobParameterTypeSet.FindAsync(id);
         if (scheduleJobParameterType == null)
         {
            return NotFound();
         }

         _context.ScheduleJobParameterTypeSet.Remove(scheduleJobParameterType);
         await _context.SaveChangesAsync();

         return scheduleJobParameterType;
      }

      private bool ScheduleJobParameterTypeExists(int id)
      {
         return _context.ScheduleJobParameterTypeSet.Any(e => e.Id == id);
      }
   }
}
