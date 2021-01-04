using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;
        private readonly SchedulingPlanningAppSettings _planningAppSettings;

        private IQueryable<Schedule> BaseQuery =>
            _context.Schedules
                .Include(s => s.Owner)
                .Include(s => s.Sender)
                .Include(s => s.ScheduleReceiverMappings)
                    .ThenInclude(m => m.Receiver)
                .Include(s => s.ScheduleWeekdayMappings)
                .Include(s => s.TimeFrames)
                .Include(s => s.ScheduleJobs)
                    .ThenInclude(j => j.Results);

        public ScheduleService(TTWebContext context,
            IMapper mapper,
            IOptions<SchedulingAppSettings> schedulingAppSettings)
        {
            _context = context;
            _mapper = mapper;
            _planningAppSettings = schedulingAppSettings.Value.Planning;
        }

        public async Task<ScheduleModel> CreateAsync(ScheduleModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var schedule = _mapper.Map<Schedule>(model);
            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();

            await _context.Entry(schedule).Reference(s => s.Sender).LoadAsync();
            await _context.Entry(schedule).Collection(s => s.ScheduleReceiverMappings).LoadAsync();
            return _mapper.Map(schedule, model);
        }

        public async Task<ScheduleModel> UpdateAsync(ScheduleModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var schedule = await BaseQuery
                .FilterById(model.Id)
                .SingleOrDefaultAsync();

            if (schedule == null) throw new ResourceNotFoundException(nameof(Data.Models.Schedule), model.Id);
            if (schedule.OwnerId != model.OwnerId) throw new UnauthorizedAccessException();

            schedule = _mapper.Map(model, schedule);
            _context.Entry(schedule).Property(s => s.PlanningStatus).IsModified = false;
            _context.Entry(schedule).Property(s => s.WorkerId).IsModified = false;
            await _context.SaveChangesAsync();

            await _context.Entry(schedule).Reference(s => s.Sender).LoadAsync();
            await _context.Entry(schedule).Collection(s => s.ScheduleReceiverMappings).LoadAsync();
            return _mapper.Map(schedule, model);
        }

        public async Task DeleteAsync(int id, int? ownerId)
        {
            var schedule = await BaseQuery
                .FilterById(id)
                .FilterByOwnerId(ownerId)
                .SingleOrDefaultAsync();

            if (schedule == null) return;

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task<ScheduleModel> ReadByIdAsync(int id, int? ownerId)
        {
            var schedule = await BaseQuery
                .AsNoTracking()
                .FilterById(id)
                .FilterByOwnerId(ownerId)
                .SingleOrDefaultAsync();

            return _mapper.Map<ScheduleModel>(schedule);
        }

        public async Task<IEnumerable<ScheduleModel>> ReadAsync()
        {
            return await BaseQuery
                .AsNoTracking()
                .Select(s => _mapper.Map<ScheduleModel>(s))
                .ToListAsync();
        }

        public async Task<IEnumerable<ScheduleModel>> PeekAsync(int count)
        {
            return await BaseQuery
                .FilterOpenSchedules(DateTime.UtcNow)
                .Take(count > 0 ? count : _planningAppSettings.CountPerRequest)
                .Select(s => _mapper.Map<ScheduleModel>(s))
                .ToListAsync();
        }
    }
}