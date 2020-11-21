using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
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
        private readonly IScheduleJobResultService _scheduleJobResultService;

        public ScheduleJobService(TTWebContext context,
            IMapper mapper, 
            IOptions<SchedulingJobAppSettings> jobAppSettingsOption, 
            IScheduleJobResultService scheduleJobResultService)
        {
            _context = context;
            _mapper = mapper;
            _scheduleJobResultService = scheduleJobResultService;
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
                .Take(_jobAppSettings.CountPerRequest)
                .Select(j => _mapper.Map<ScheduleJobModel>(j))
                .ToListAsync();
        }

        public async Task<IEnumerable<ScheduleJobModel>> PeekLockAsync()
        {
            var jobs = await BaseQuery
                .FilterOpenJobs()
                .Take(_jobAppSettings.CountPerRequest)
                .ToListAsync();

            var now = DateTime.UtcNow;
            jobs.ForEach(j => j.Lock(now));
            await _context.SaveChangesAsync();

            return jobs.Select(j => _mapper.Map<ScheduleJobModel>(j));
        }

        public async Task UpdateStatusAsync(int id, ProcessingResult<ScheduleJobModel> result)
        {
            _context.ScheduleJobs.Attach(new ScheduleJob
            {
                Id = id, 
                Status = result.Succeed ? ProcessingStatus.Completed : ProcessingStatus.Error 
            });
            await _context.SaveChangesAsync();

            await _scheduleJobResultService.CreateAsync(new ScheduleJobResultModel {ScheduleJobId = id});
        }

        private IQueryable<ScheduleJob> BaseQuery =>
            _context.ScheduleJobs
            .Include(j => j.Sender)
            .Include(j => j.Receiver);
    }
}
