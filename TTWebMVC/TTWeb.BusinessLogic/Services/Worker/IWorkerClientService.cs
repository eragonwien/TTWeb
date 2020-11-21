using System.Threading.Tasks;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public interface IWorkerClientService
    {
        Task TriggerPlanningAsync();
    }
}