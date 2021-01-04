using System.Threading;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public interface IFacebookAutomationService
    {
        Task<bool> ProcessAsync(ScheduleJobModel workingJob, CancellationToken cancellationToken);
    }
}