﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Services.Schedule;
using TTWeb.Data.Models;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/schedules")]
    [ApiController]
    public class ScheduleController : BaseController
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost]
        public async Task<ScheduleModel> Create([FromBody] ScheduleModel model)
        {
            return await _scheduleService.CreateAsync(model);
        }

        [HttpGet("{id}")]
        public async Task<ScheduleModel> ReadOne([FromRoute] int id)
        {
            var schedule = await _scheduleService.ReadByIdAsync(id, OwnerId);
            ThrowExceptionOnUnauthorizedAccess(schedule?.OwnerId);
            return schedule;
        }

        [HttpGet("")]
        [Authorize(Policy = Startup.RequireAccessAllResourcesPermissionPolicy)]
        public async Task<IEnumerable<ScheduleModel>> Read()
        {
            return await _scheduleService.ReadAsync();
        }

        [HttpPatch("{id}")]
        public async Task<ScheduleModel> Update([FromRoute] int id, [FromBody] ScheduleModel model)
        {
            if (id != model.Id) throw new InvalidInputException(nameof(model.Id));

            return await _scheduleService.UpdateAsync(model);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _scheduleService.DeleteAsync(id, OwnerId);
        }

        [HttpGet("peek")]
        [Authorize(Policy = Startup.RequireWorkerPermissionPolicy)]
        public async Task<IEnumerable<ScheduleModel>> Peek([FromQuery] int count, [FromQuery] ProcessingStatus status)
        {
            return await _scheduleService.PeekAsync(count, status);
        }

        [HttpPost("trigger-planning")]
        [Authorize(Policy = Startup.RequireWorkerPermissionPolicy)]
        public async Task TriggerPlanning()
        {
            await _scheduleService.PlanAsync();
        }
    }
}
