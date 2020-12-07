using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.Worker.ScheduleExecutor.Services
{
    public interface IFacebookAutomationService
    {
        Task ProcessAsync(ScheduleJobModel workingJob);
    }
}
