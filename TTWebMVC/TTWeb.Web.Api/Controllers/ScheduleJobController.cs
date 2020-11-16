using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.BusinessLogic.Services.Schedule;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/schedule-jobs")]
    [ApiController]
    public class ScheduleJobController : BaseController
    {
        private readonly IScheduleJobService _scheduleJobService;

        public ScheduleJobController(IScheduleJobService scheduleJobService)
        {
            _scheduleJobService = scheduleJobService;
        }

        [HttpGet("peek")]
        public async Task<IEnumerable<ScheduleJobModel>> Peek()
        {
            return await _scheduleJobService.PeekAsync();
        }

        [HttpPost("peek-lock")]
        public async Task<IEnumerable<ScheduleJobModel>> PeekLock()
        {
            return await _scheduleJobService.PeekLockAsync();
        }

        [HttpPatch("{id}/status")]
        public async Task UpdateStatus([FromRoute] int id, [FromBody] ProcessingResult<ScheduleJobModel> result)
        {
            await _scheduleJobService.UpdateStatusAsync(id, result);
        }
    }
}
