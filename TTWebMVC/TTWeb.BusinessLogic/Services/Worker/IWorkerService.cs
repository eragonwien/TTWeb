using System.Threading.Tasks;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public interface IWorkerService
    {
        Task TriggerPlanningAsync();
    }
}