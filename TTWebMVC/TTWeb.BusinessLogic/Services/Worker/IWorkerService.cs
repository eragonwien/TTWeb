using System.Threading.Tasks;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public interface IWorkerService
    {
        Task<Data.Models.Worker> FindAsync(int id, string secret);
    }
}
