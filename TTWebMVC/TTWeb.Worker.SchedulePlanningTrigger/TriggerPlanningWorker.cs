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
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly SchedulingAppSettings settings;

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
                var planningStartTime = DateTime.UtcNow;
                _logger.LogInformation($"Worker running at: {planningStartTime}");

                using var scope = _scopeFactory.CreateScope();
                await using var context = scope.ServiceProvider.GetRequiredService<TTWebContext>();
                await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

                var schedules = await context.Schedules
                    .Include(s => s.ScheduleReceiverMappings)
                    .Include(s => s.ScheduleWeekdayMappings)
                    .Include(s => s.TimeFrames)
                    .Include(s => s.Sender)
                    .FilterOpenSchedules(planningStartTime)
                    .OrderBy(s => s.Id)
                    .ToListAsync(cancellationToken);

                if (schedules.Count == 0)
                {
                    _logger.LogInformation($"No open schedule found at {planningStartTime}");
                    _logger.LogInformation(
                        $"Next session starts at {DateTime.UtcNow.Add(settings.Planning.TriggerInterval)}");
                    await Task.Delay(settings.Planning.TriggerInterval, cancellationToken);
                    continue;
                }

                foreach (var schedule in schedules)
                    schedule
                        .Lock(planningStartTime, settings.Planning.LockDuration)
                        .SetStatus(ProcessingStatus.InProgress);

                await context.SaveChangesAsync(cancellationToken);

                var planningResults = CalculateJobStartTime(schedules);
                var successResults = planningResults.Where(r => r.Succeed).Select(r => r.Result);
                var scheduleJobs = await AddScheduleJobsAsync(successResults, context, cancellationToken);
                await UpdateScheduleStatusAsync(schedules, scheduleJobs, context, cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation($"Planning at {planningStartTime} is completed");
                _logger.LogInformation(
                    $"Next session starts at {DateTime.UtcNow.Add(settings.Planning.TriggerInterval)}");
                await Task.Delay(settings.Planning.TriggerInterval, cancellationToken);
            }
        }

        private IEnumerable<ProcessingResult<ScheduleJobModel>> CalculateJobStartTime(List<Schedule> schedules)
        {
            if (schedules == null) throw new ArgumentNullException(nameof(schedules));

            var results = new List<ProcessingResult<ScheduleJobModel>>();

            foreach (var schedule in schedules)
            {
                var result = new ProcessingResult<ScheduleJobModel>();
                try
                {
                    var receiverIds = schedule.ScheduleReceiverMappings
                        .Select(m => m.ReceiverId)
                        .Distinct()
                        .ToList();

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

        public async Task<List<ScheduleJob>> AddScheduleJobsAsync(
            IEnumerable<ScheduleJobModel> models,
            TTWebContext context,
            CancellationToken cancellationToken)
        {
            if (models == null) throw new ArgumentNullException(nameof(models));

            var jobs = models.Select(m => _mapper.Map<ScheduleJob>(m).WithStatus(ProcessingStatus.New)).ToList();
            await context.ScheduleJobs.AddRangeAsync(jobs, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return jobs;
        }

        private async Task UpdateScheduleStatusAsync(
            List<Schedule> schedules,
            List<ScheduleJob> scheduleJobs,
            TTWebContext context,
            CancellationToken cancellationToken)
        {
            if (schedules == null) throw new ArgumentNullException(nameof(schedules));
            if (scheduleJobs == null) throw new ArgumentNullException(nameof(scheduleJobs));

            var completionDate = DateTime.UtcNow;

            foreach (var schedule in schedules)
            {
                var succeed = scheduleJobs.Any(j => j.ScheduleId == schedule.Id);
                schedule.PlanningStatus = succeed ? ProcessingStatus.Completed : ProcessingStatus.Error;

                if (succeed)
                    schedule.CompletedAt = completionDate;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}