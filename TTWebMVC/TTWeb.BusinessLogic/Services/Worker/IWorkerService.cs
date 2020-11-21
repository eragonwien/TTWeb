using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public interface IWorkerService
    {
        Task<Data.Models.Worker> FindAsync(int id, string secret);
        Task<WorkerModel> GenerateAsync();
        Task DeleteAsync();
    }
}
