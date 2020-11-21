using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Database;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public class WorkerService : IWorkerService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;

        public WorkerService(TTWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Data.Models.Worker> FindAsync(int id, string secret)
        {
            if (id <= 0) throw new ArgumentException(nameof(id));
            if (string.IsNullOrWhiteSpace(secret)) throw new ArgumentException(nameof(secret));

            return await _context.Workers.SingleOrDefaultAsync(w => w.Id == id && w.Secret == secret);
        }

        public async Task<WorkerModel> GenerateAsync()
        {
            var worker = await _context.Workers.AddAsync(new Data.Models.Worker());
            return _mapper.Map<WorkerModel>(worker);
        }

        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}