using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;
using TTWeb.Data.Models;

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
                .Include(s => s.ScheduleJobs)
                    .ThenInclude(j => j.Results);

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
            await _context.ScheduleReceiverMappings.AddRangeAsync(schedule.ScheduleReceiverMappings);
            await _context.ScheduleWeekdayMappings.AddRangeAsync(schedule.ScheduleWeekdayMappings);
            await _context.ScheduleTimeFrames.AddRangeAsync(schedule.TimeFrames);
            await _context.SaveChangesAsync();

            return _mapper.Map(schedule, model);
        }

        public async Task<ScheduleModel> UpdateAsync(ScheduleModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var schedule = await BaseQuery
                .FilterById(model.Id)
                .SingleOrDefaultAsync();

            if (schedule == null) throw new ResourceNotFoundException(nameof(Data.Models.Schedule), model.Id);

            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                schedule = _mapper.Map(model, schedule);
                await _context.SaveChangesAsync();

                //await _context.ScheduleReceiverMappings.AddRangeAsync(model.Receivers
                //    .Except(schedule.ScheduleReceiverMappings.Select(rm => _mapper.Map<ScheduleReceiverModel>(rm)))
                //    .Select(receiver => new ScheduleReceiverMapping
                //    {
                //        ReceiverId = receiver.Id,
                //        ScheduleId = model.Id
                //    }));

                //await _context.ScheduleWeekdayMappings.AddRangeAsync(model.Weekdays
                //    .Except(schedule.ScheduleWeekdayMappings.Select(dm => _mapper.Map<DayOfWeek>(dm)))
                //    .Select(weekday => new ScheduleWeekdayMapping
                //    {
                //        Weekday = weekday,
                //        ScheduleId = model.Id
                //    }));

                //await _context.ScheduleTimeFrames.AddRangeAsync(model.TimeFrames
                //    .Except(schedule.TimeFrames.Select(tf => _mapper.Map<ScheduleTimeFrameModel>(tf)))
                //    .Select(timeFrame => new ScheduleTimeFrame
                //    {
                //        From = timeFrame.From,
                //        To = timeFrame.To,
                //        ScheduleId = model.Id
                //    }));

                await _context.SaveChangesAsync();

                await trans.CommitAsync();
            }

            return _mapper.Map(schedule, model);
        }

        public async Task DeleteAsync(int id, int? ownerId)
        {
            var schedule = await BaseQuery
                .FilterById(id)
                .FilterByOwnerId(ownerId)
                .SingleOrDefaultAsync();

            if (schedule == null) return;

            _context.ScheduleJobsResults.RemoveRange(schedule.ScheduleJobs.SelectMany(j => j.Results));
            _context.ScheduleJobs.RemoveRange(schedule.ScheduleJobs);
            _context.ScheduleReceiverMappings.RemoveRange(schedule.ScheduleReceiverMappings);
            _context.ScheduleWeekdayMappings.RemoveRange(schedule.ScheduleWeekdayMappings);
            _context.ScheduleTimeFrames.RemoveRange(schedule.TimeFrames);

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
    }
}