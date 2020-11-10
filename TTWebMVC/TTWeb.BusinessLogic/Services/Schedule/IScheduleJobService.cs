using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services.Schedule
{
    public interface IScheduleJobService
    {
        Task CreateAsync(ScheduleJobModel model);
    }
}
