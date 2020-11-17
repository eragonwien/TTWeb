using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Models.Entities;
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

        [HttpPost("peek-lock")]
        [Authorize(Policy = Startup.RequireWorkerPermissionPolicy)]
        public async Task<IEnumerable<ScheduleJobModel>> PeekLock()
        {
            return await _scheduleJobService.PeekLockAsync(LoginUserId);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Policy = Startup.RequireWorkerPermissionPolicy)]
        public async Task UpdateStatus([FromRoute] int id, [FromBody] ScheduleJobModel model)
        {
            if (id != LoginUserId || LoginUserId != model.WorkerId) throw new UnauthorizedAccessException();

            await _scheduleJobService.UpdateStatusAsync(model);
        }

        [HttpGet("peek")]
        [Authorize(Policy = Startup.RequireWorkerPermissionPolicy)]
        public async Task<IEnumerable<ScheduleJobModel>> Peek()
        {
            return await _scheduleJobService.PeekAsync();
        }
    }
}
