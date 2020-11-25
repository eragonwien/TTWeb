using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public interface IWorkerClientService
    {
        Task<int> TriggerPlanningAsync();
        Task<List<ScheduleJobModel>> FetchJobsAsync();
    }
}