using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services
{
    public interface IScheduleJobResultService
    {
        Task CreateAsync(ScheduleJobResultModel scheduleJobResultModel);
    }
}