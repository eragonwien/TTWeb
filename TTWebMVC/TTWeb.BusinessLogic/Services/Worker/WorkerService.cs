using System.Threading.Tasks;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public class WorkerService : IWorkerService
    {
        public Task<Data.Models.Worker> FindAsync(int id, string secret)
        {
            throw new System.NotImplementedException();
        }
    }
}