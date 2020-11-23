using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Database;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services.Worker
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

        public async Task<Data.Models.Worker> FindAsync(int id, string secret)
        {
            if (id <= 0) throw new ArgumentException(nameof(id));
            if (string.IsNullOrWhiteSpace(secret)) throw new ArgumentException(nameof(secret));

            return await _context.Workers
                .Include(w => w.WorkerPermissionMappings)
                .SingleOrDefaultAsync(w => w.Id == id && w.Secret == secret);
        }

        public async Task<WorkerModel> GenerateAsync()
        {
            var worker = new Data.Models.Worker
            {
                Secret = _helperService.GetRandomString(128),
                WorkerPermissionMappings = new List<WorkerPermissionMapping> { new WorkerPermissionMapping { UserPermission = UserPermission.IsWorker } }
            };
            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();

            return _mapper.Map<WorkerModel>(worker);
        }

        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}