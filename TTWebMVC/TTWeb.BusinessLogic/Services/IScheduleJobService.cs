using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface IScheduleJobService
    {
        List<ProcessingResult<ScheduleJobModel>> PlanJob(IEnumerable<Schedule> schedules);

        Task<IEnumerable<ScheduleJob>> CreateAsync(IEnumerable<ScheduleJobModel> models);

        Task<ICollection<ScheduleJobModel>> PeekAsync();

        Task UpdateStatusAsync(int id, ProcessingResult<ScheduleJobModel> result);
    }
}