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
            _context.Schedules.Include(s => s.Owner).AsNoTracking();

        public ScheduleService(TTWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ScheduleModel> CreateAsync(ScheduleModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var schedule = _mapper.Map<Data.Models.Schedule>(model);

            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();

            return _mapper.Map(schedule, model);
        }

        public async Task<ScheduleModel> UpdateAsync(ScheduleModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var schedule = await BaseQuery.FilterById(model.Id).SingleOrDefaultAsync();
            if (schedule == null) throw new ArgumentNullException(nameof(schedule));

            schedule = _mapper.Map(model, schedule);
            await _context.SaveChangesAsync();

            return _mapper.Map(schedule, model);
        }

        public async Task DeleteAsync(int id, int? ownerId)
        {
            var existingUserModel = await ReadByIdAsync(id, ownerId);
            if (existingUserModel == null) return;

            var schedule = new Data.Models.Schedule { Id = id };
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