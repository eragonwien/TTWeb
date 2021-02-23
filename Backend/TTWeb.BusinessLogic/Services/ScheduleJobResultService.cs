using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TTWeb.BusinessLogic.Models;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public class ScheduleJobResultService : IScheduleJobResultService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;

        public ScheduleJobResultService(TTWebContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private IQueryable<ScheduleJobResult> BaseQuery =>
            _context.ScheduleJobsResults
                .Include(u => u.ScheduleJob);

        public async Task CreateAsync(ScheduleJobResultModel scheduleJobResultModel)
        {
            if (scheduleJobResultModel == null) throw new ArgumentNullException(nameof(scheduleJobResultModel));

            var jobResult = _mapper.Map<ScheduleJobResult>(scheduleJobResultModel);
            await _context.ScheduleJobsResults.AddAsync(jobResult);
            await _context.SaveChangesAsync();
        }

        public async Task<ScheduleJobResultModel> ReadOneAsync(int id)
        {
            var entity = await BaseQuery
                .AsNoTracking()
                .FilterById(id)
                .SingleOrDefaultAsync();

            return _mapper.Map<ScheduleJobResultModel>(entity);
        }

        public async Task<IEnumerable<ScheduleJobResultModel>> ReadManyAsync(int scheduleJobId)
        {
            var entities = await BaseQuery
                .AsNoTracking()
                .Where(e => e.ScheduleJobId == scheduleJobId)
                .ToArrayAsync();

            return entities.Select(entity => _mapper.Map<ScheduleJobResultModel>(entity));
        }

        public async Task DeleteOneAsync(int id, int? ownerId)
        {
            // TODO: Filters owner
            var entity = await BaseQuery
                .FilterById(id)
                .SingleOrDefaultAsync();

            if (entity == null) return;

            _context.ScheduleJobsResults.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}