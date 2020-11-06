using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;

namespace TTWeb.BusinessLogic.Services.Schedule
{
    public class ScheduleService : IScheduleService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;

        private IQueryable<Data.Models.Schedule> BaseQuery =>
            _context.Schedules
            .Include(s => s.Owner)
            .Include(s => s.ScheduleReceiverMappings)
            .Include(s => s.ScheduleWeekdayMappings)
            .Include(s => s.TimeFrames)
            .AsNoTracking();

        public ScheduleService(TTWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ScheduleModel> CreateAsync(ScheduleModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var schedule = _mapper.Map<Data.Models.Schedule>(model);

            // TODO: Adds everything in one transaction
            // TODO: Adds/Updates receiver mappings
            // TODO: Adds/Updates weekday mappings
            // TODO: Adds/Updates time frames mappings
            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();

            return _mapper.Map(schedule, model);
        }

        public async Task<ScheduleModel> UpdateAsync(ScheduleModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var schedule = await BaseQuery.FilterById(model.Id).SingleOrDefaultAsync();
            if (schedule == null) throw new ArgumentNullException(nameof(schedule));

            // TODO: Updates everything in one transaction
            // TODO: Updates receiver mappings
            // TODO: Updates weekday mappings
            // TODO: Updates time frames mappings
            _context.Schedules.Attach(schedule);
            schedule = _mapper.Map(model, schedule);
            await _context.SaveChangesAsync();

            return _mapper.Map(schedule, model);
        }

        public async Task DeleteAsync(int id, int? ownerId)
        {
            var schedule = new Data.Models.Schedule { Id = id };
            if (ownerId.HasValue)
                schedule.OwnerId = ownerId.Value;

            // TODO: Updates everything in one transaction
            // TODO: Updates receiver mappings
            // TODO: Updates weekday mappings
            // TODO: Updates time frames mappings
            _context.Schedules.Attach(schedule);
            _context.Schedules.Remove(schedule);

            await _context.SaveChangesAsync();
        }

        public async Task<ScheduleModel> ReadByIdAsync(int id, int? ownerId)
        {
            var schedule = await BaseQuery
                .FilterById(id)
                .FilterByOwnerId(ownerId)
                .SingleOrDefaultAsync();

            return _mapper.Map<ScheduleModel>(schedule);
        }

        public async Task<IEnumerable<ScheduleModel>> ReadAsync()
        {
            return await BaseQuery
                .Select(s => _mapper.Map<ScheduleModel>(s))
                .ToListAsync();
        }
    }
}