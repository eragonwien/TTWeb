using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.Worker.Core.Services
{
    public interface IWorkerClientService
    {
        Task<int> TriggerPlanningAsync();

        Task<List<ScheduleJobModel>> FetchJobsAsync();
    }
}