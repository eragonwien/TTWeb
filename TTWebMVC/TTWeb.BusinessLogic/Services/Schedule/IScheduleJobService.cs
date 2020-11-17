using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services.Schedule
{
    public interface IScheduleJobService
    {
        List<ProcessingResult<ScheduleJobModel>> PlanJob(IEnumerable<Data.Models.Schedule> schedules);
        Task<IEnumerable<ScheduleJob>> CreateAsync(IEnumerable<ScheduleJobModel> models);
        Task<List<ScheduleJobModel>> PeekAsync();
        Task<List<ScheduleJobModel>> PeekLockAsync(int workerId);
        Task UpdateStatusAsync(ScheduleJobModel model);
    }
}
