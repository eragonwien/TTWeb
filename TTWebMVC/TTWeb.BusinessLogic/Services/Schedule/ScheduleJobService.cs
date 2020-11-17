using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services.Schedule
{
    public class ScheduleJobService : IScheduleJobService
    {
        private readonly TTWebContext _context;
        private readonly IMapper _mapper;
        private readonly SchedulingJobAppSettings _jobAppSettings;

        public ScheduleJobService(TTWebContext context, IMapper mapper, IOptions<SchedulingJobAppSettings> jobAppSettingsOption)
        {
            _context = context;
            _mapper = mapper;
            _jobAppSettings = jobAppSettingsOption.Value;
        }

        public List<ProcessingResult<ScheduleJobModel>> PlanJob(IEnumerable<Data.Models.Schedule> schedules)
        {
            if (schedules == null) throw new ArgumentNullException(nameof(schedules));

            var results = new List<ProcessingResult<ScheduleJobModel>>();

            foreach (var schedule in schedules)
            {
                var result = new ProcessingResult<ScheduleJobModel>();
                try
                {
                    var receiverIds = schedule.ScheduleReceiverMappings.Select(m => m.ReceiverId).Distinct().ToList();
                    var receiverTimeFrames = receiverIds.Zip(schedule.TimeFrames, Tuple.Create);
                    foreach (var (receiverId, timeFrame) in receiverTimeFrames)
                    {
                        result.Result = _mapper.Map<ScheduleJobModel>(schedule)
                            .WithReceiver(receiverId)
                            .WithTimeFrame(timeFrame)
                            .CalculateStartTime();

                        result.Succeed = true;
                    }
                }
                catch (Exception ex)
                {
                    result.Succeed = false;
                    result.Exception = ex;
                    result.Message = $"Error occurs during calculation of schedule {schedule.Id}";
                }
                results.Add(result);
            }

            return results;
        }

        public async Task<IEnumerable<ScheduleJob>> CreateAsync(IEnumerable<ScheduleJobModel> models)
        {
            if (models == null) throw new ArgumentNullException(nameof(models));

            var jobs = models.Select(m => _mapper.Map<ScheduleJob>(m).WithStatus(ProcessingStatus.New)).ToList();
            await _context.ScheduleJobs.AddRangeAsync(jobs);
            await _context.SaveChangesAsync();
            return jobs;
        }

        public async Task<IEnumerable<ScheduleJobModel>> PeekAsync()
        {
            return await BaseQuery
                .AsNoTracking()
                .FilterOpenJobs()
                .Take(_jobAppSettings.PeekCount)
                .Select(j => _mapper.Map<ScheduleJobModel>(j))
                .ToListAsync();
        }

        public async Task<IEnumerable<ScheduleJobModel>> PeekLockAsync()
        {
            var jobs = await PeekAsync();

            _context.AttachRange(jobs.Select(j => new ScheduleJob { Id = j.Id, Status = ProcessingStatus.InProgress }));
            await _context.SaveChangesAsync();

            return jobs;
        }

        private IQueryable<ScheduleJob> BaseQuery =>
            _context.ScheduleJobs
            .Include(j => j.Sender)
            .Include(j => j.Receiver);
    }
}
