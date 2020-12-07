using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services.Schedule
{
    public interface IScheduleJobResultService
    {
        Task CreateAsync(ScheduleJobResultModel scheduleJobResultModel);
    }
}