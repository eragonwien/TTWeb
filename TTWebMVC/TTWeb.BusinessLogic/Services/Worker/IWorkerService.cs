using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public interface IWorkerService
    {
        Task<WorkerModel> FindAsync(int id, string secret);
        Task<WorkerModel> GenerateAsync();
        Task DeleteAsync(int id);
        Task<List<WorkerModel>> ReadAsync();
    }
}
