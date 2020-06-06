using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
         return await jobService.GetScheduleJobDefs(ContextUser.Id).ToListAsync();
      }

      // GET: api/ScheduleJob/5
      [HttpGet("def/{id}")]
      public async Task<ActionResult<ScheduleJobDef>> GetScheduleJobDef(int id)
      {
         var scheduleJob = await jobService.GetScheduleJobDef(id, ContextUser.Id);

         if (scheduleJob == null)
         {
            return NotFound();
         }

         return scheduleJob;
      }

      // PUT: api/ScheduleJob/5
      [HttpPut("def/{id}")]
      public Task<IActionResult> UpdateScheduleJobDef(ScheduleJobDef scheduleJobDef)
      {
         throw new NotImplementedException();
      }

      // POST: api/ScheduleJob
      [HttpPost("def")]
      public Task<ActionResult<ScheduleJobDef>> CreateScheduleJobDef(ScheduleJobDef scheduleJobDef)
      {
         throw new NotImplementedException();
      }

      // DELETE: api/ScheduleJob/5
      [HttpDelete("def/{id}")]
      public async Task<ActionResult<ScheduleJobDef>> DeleteScheduleJobDef(int id)
      {
         await jobService.RemoveScheduleJobDef(id, ContextUser.Id);
         await jobService.SaveChangesAsync();
         return NoContent();
      }
   }
}
