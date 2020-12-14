﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface IScheduleJobService
    {
        List<ProcessingResult<ScheduleJobModel>> PlanJob(IEnumerable<Data.Models.Schedule> schedules);

        Task<IEnumerable<ScheduleJob>> CreateAsync(IEnumerable<ScheduleJobModel> models);

        Task<IEnumerable<ScheduleJobModel>> PeekAsync();

        Task<IEnumerable<ScheduleJobModel>> PeekLockAsync();

        Task UpdateStatusAsync(int id, ProcessingResult<ScheduleJobModel> result);
    }
}