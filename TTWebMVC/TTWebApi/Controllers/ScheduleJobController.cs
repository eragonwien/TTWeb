using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTWebApi.Services;
using TTWebCommon.Models;

namespace TTWebApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ScheduleJobController : BaseController
   {
      private readonly IScheduleJobService jobService;

      public ScheduleJobController(IScheduleJobService jobService)
      {
         this.jobService = jobService;
      }

      // GET: api/ScheduleJob
      [HttpGet("def")]
      public async Task<ActionResult<List<ScheduleJobDef>>> GetScheduleJobDefs()
      {
         return await jobService.GetScheduleJobDefs()
            .Where(d => d.AppUserId == ContextUser.Id)
            .ToListAsync();
      }

      // GET: api/ScheduleJob/def/5
      [HttpGet("def/{id}")]
      public async Task<ActionResult<ScheduleJobDef>> GetScheduleJobDef(int id)
      {
         var scheduleJob = await jobService.GetScheduleJobDef(id)
            .Where(d => d.AppUserId == ContextUser.Id)
            .SingleOrDefaultAsync();

         if (scheduleJob == null)
         {
            return NotFound();
         }

         return scheduleJob;
      }

      // PUT: api/ScheduleJob/def/5
      [HttpPut("def/{id}")]
      public Task<IActionResult> UpdateScheduleJobDef(ScheduleJobDef scheduleJobDef)
      {
         throw new NotImplementedException();
      }

      // POST: api/ScheduleJob/def
      [HttpPost("def")]
      public Task<ActionResult<ScheduleJobDef>> CreateScheduleJobDef(ScheduleJobDef scheduleJobDef)
      {
         throw new NotImplementedException();
      }

      // DELETE: api/ScheduleJob/def/1
      [HttpDelete("def/{id}")]
      public async Task<ActionResult<ScheduleJobDef>> DeleteScheduleJobDef(int id)
      {
         await jobService.RemoveScheduleJobDef(id, ContextUser.Id);
         await jobService.SaveChangesAsync();
         return NoContent();
      }

      // GET: api/ScheduleJob/types
      [HttpGet("types")]
      public ActionResult<IEnumerable<string>> GetAvailableScheduleJobTypes()
      {
         var types = Enum.GetNames(typeof(ScheduleJobType)).AsEnumerable();
         return Ok(types);
      }

      // GET: api/ScheduleJob/intervaltypes
      [HttpGet("intervaltypes")]
      public ActionResult<IEnumerable<string>> GetAvailableScheduleJobIntevalTypes()
      {
         var types = Enum.GetNames(typeof(IntervalType)).AsEnumerable();
         return Ok(types);
      }
   }
}
