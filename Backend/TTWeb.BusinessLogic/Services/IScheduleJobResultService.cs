using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface IScheduleJobResultService
    {
        Task CreateAsync(ScheduleJobResultModel scheduleJobResultModel);

        Task<ScheduleJobResultModel> ReadOneAsync(int id);

        Task<IEnumerable<ScheduleJobResultModel>> ReadManyAsync(int scheduleJobId);

        Task DeleteOneAsync(int id, int? ownerId);
    }
}