using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface IScheduleJobResultService
    {
        Task CreateAsync(ScheduleJobResultModel scheduleJobResultModel);
    }
}