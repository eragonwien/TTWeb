using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Database;

namespace TTWeb.BusinessLogic.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;
        private readonly IHelperService _helperService;

        public WorkerService(TTWebContext context,
            IMapper mapper,
            IHelperService helperService)
        {
            _context = context;
            _mapper = mapper;
            _helperService = helperService;
        }

        public async Task<WorkerModel> FindAsync(int id, string secret)
        {
            if (id <= 0) throw new ArgumentException(nameof(id));
            if (string.IsNullOrWhiteSpace(secret)) throw new ArgumentException(nameof(secret));

            return await BaseQuery
                .AsNoTracking()
                .Where(w => w.Id == id && w.Secret == secret)
                .Select(w => _mapper.Map<WorkerModel>(w))
                .SingleOrDefaultAsync();
        }

        public async Task<WorkerModel> GenerateAsync()
        {
            var worker = new Data.Models.Worker()
                .WithSecret(_helperService.GetRandomString(128))
                .CreatedAt(DateTime.UtcNow)
                .WithDefaultPermissions();

            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();

            return _mapper.Map<WorkerModel>(worker);
        }

        public async Task DeleteAsync(int id)
        {
            var worker = new Data.Models.Worker { Id = id };
            _context.Workers.Attach(worker);
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WorkerModel>> ReadAsync()
        {
            return await BaseQuery
                .AsNoTracking()
                .Select(w => _mapper.Map<WorkerModel>(w))
                .ToListAsync();
        }

        private IQueryable<Data.Models.Worker> BaseQuery => _context.Workers.Include(w => w.WorkerPermissionMappings);
    }
}