using System.Threading;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public interface IFacebookAutomationService
    {
        Task<ProcessingResult<ScheduleJobModel>> ProcessAsync(ScheduleJobModel job, CancellationToken cancellationToken);
    }
}