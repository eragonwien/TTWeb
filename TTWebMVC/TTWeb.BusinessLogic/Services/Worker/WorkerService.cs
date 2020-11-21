using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TTWeb.Data.Database;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public class WorkerService : IWorkerService
    {
        private readonly TTWebContext _context;

        public WorkerService(TTWebContext context)
        {
            _context = context;
        }

        public async Task<Data.Models.Worker> FindAsync(int id, string secret)
        {
            if (id <= 0) throw new ArgumentException(nameof(id));
            if (string.IsNullOrWhiteSpace(secret)) throw new ArgumentException(nameof(secret));

            return await _context.Workers.SingleOrDefaultAsync(w => w.Id == id && w.Secret == secret);
        }
    }
}