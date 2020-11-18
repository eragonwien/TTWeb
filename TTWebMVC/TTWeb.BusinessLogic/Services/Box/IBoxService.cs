using System.Threading.Tasks;

namespace TTWeb.BusinessLogic.Services.Box
{
    public interface IBoxService
    {
        Task TriggerPlanningAsync();
    }
}