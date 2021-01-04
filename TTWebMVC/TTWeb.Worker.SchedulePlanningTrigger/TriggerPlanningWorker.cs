using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;
using TTWeb.Data.Models;

namespace TTWeb.Worker.SchedulePlanningTrigger
{
    public class TriggerPlanningWorker : BackgroundService
    {
        private readonly ILogger<TriggerPlanningWorker> _logger;
        private readonly SchedulingAppSettings settings;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public TriggerPlanningWorker(ILogger<TriggerPlanningWorker> logger,
            IOptions<SchedulingAppSettings> schedulingAppSettingsOptions, 
            IServiceScopeFactory scopeFactory,
            IMapper mapper)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            settings = schedulingAppSettingsOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}");

                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<TTWebContext>();

                var utcNow = DateTime.UtcNow;

                var schedules = await context.Schedules
                    .Include(s => s.ScheduleReceiverMappings)
                    .Include(s => s.ScheduleWeekdayMappings)
                    .Include(s => s.TimeFrames)
                    .FilterOpenSchedules(utcNow)
                    .OrderBy(s => s.Id)
                    .ToListAsync(cancellationToken);

                schedules.ForEach(s => s.Lock(utcNow, settings.Planning.LockDuration));
                await context.SaveChangesAsync(cancellationToken);

                var planningResults = CalculateJobStartTime(schedules);
                var successPlannedJobs = await AddPlannedJobsAsync(context, planningResults.Where(r => r.Succeed).Select(r => r.Result));
                await UpdateScheduleStatusAsync(context, schedules, successPlannedJobs.ToList());

                _logger.LogInformation($"Schedules triggered successfully at {DateTimeOffset.Now}");
                await Task.Delay(settings.Planning.TriggerInterval, cancellationToken);
            }
        }

        private List<ProcessingResult<ScheduleJobModel>> CalculateJobStartTime(List<Schedule> schedules)
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

        public async Task<IEnumerable<ScheduleJob>> AddPlannedJobsAsync(TTWebContext context,
            IEnumerable<ScheduleJobModel> models)
        {
            if (models == null) throw new ArgumentNullException(nameof(models));

            var jobs = models.Select(m => _mapper.Map<ScheduleJob>(m).WithStatus(ProcessingStatus.New)).ToList();
            await context.ScheduleJobs.AddRangeAsync(jobs);
            await context.SaveChangesAsync();
            return jobs;
        }

        private async Task UpdateScheduleStatusAsync(TTWebContext context, 
            List<Schedule> schedules,
            List<ScheduleJob> successPlannedJobs)
        {
            if (schedules == null) throw new ArgumentNullException(nameof(schedules));
            if (successPlannedJobs == null) throw new ArgumentNullException(nameof(successPlannedJobs));

            foreach (var schedule in schedules)
            {
                schedule.PlanningStatus = successPlannedJobs.Any(j => j.ScheduleId == schedule.Id)
                    ? ProcessingStatus.Completed
                    : ProcessingStatus.Error;
            }

            await context.SaveChangesAsync();
        }
    }
}
