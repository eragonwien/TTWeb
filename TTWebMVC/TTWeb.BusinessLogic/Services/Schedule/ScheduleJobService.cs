﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;

namespace TTWeb.BusinessLogic.Services.Schedule
{
    public class ScheduleJobService : IScheduleJobService
    {
        public Task<List<ProcessingResult<ScheduleJobModel>>> PlanJobAsync(IEnumerable<Data.Models.Schedule> schedules)
        {
            throw new System.NotImplementedException();
        }
    }
}
