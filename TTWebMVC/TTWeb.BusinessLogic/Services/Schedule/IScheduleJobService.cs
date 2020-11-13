using System.Collections.Generic;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;

namespace TTWeb.BusinessLogic.Services.Schedule
{
    public interface IScheduleJobService
    {
        List<ProcessingResult<ScheduleJobModel>> PlanJob(IEnumerable<Data.Models.Schedule> schedules);
    }
}
