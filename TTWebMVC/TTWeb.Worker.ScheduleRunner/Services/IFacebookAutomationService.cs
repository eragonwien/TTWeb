using System.Threading;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public interface IFacebookAutomationService
    {
        Task<ProcessingResult<ScheduleJobModel>> ProcessAsync(ScheduleJobModel job, CancellationToken cancellationToken);
    }
}