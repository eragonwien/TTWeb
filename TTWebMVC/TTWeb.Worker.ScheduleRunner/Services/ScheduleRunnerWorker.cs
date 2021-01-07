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

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public class ScheduleRunnerWorker : BackgroundService
    {
        private readonly IFacebookAutomationService _facebookService;
        private readonly ILogger<ScheduleRunnerWorker> _logger;
        private readonly IMapper _mapper;
        private readonly SchedulingAppSettings _schedulingAppSettings;
        private readonly IServiceScopeFactory _scopeFactory;

        public ScheduleRunnerWorker(ILogger<ScheduleRunnerWorker> logger,
            IOptions<SchedulingAppSettings> schedulingAppSettingsOptions,
            IFacebookAutomationService facebookService,
            IServiceScopeFactory scopeFactory,
            IMapper mapper)
        {
            _logger = logger;
            _schedulingAppSettings = schedulingAppSettingsOptions.Value;
            _facebookService = facebookService;
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}");

                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<TTWebContext>();
                var queue = await EnqueueJobsAsync(context, cancellationToken);

                while (queue.TryDequeue(out var job) && !cancellationToken.IsCancellationRequested)
                {
                    var result = await _facebookService.ProcessAsync(job, cancellationToken);
                    job = await UpdateStatusAsync(context, job, result, cancellationToken);
                    await CreateScheduleJobResultAsync(context, job, cancellationToken);
                }

                await Task.Delay(_schedulingAppSettings.Job.TriggerInterval, cancellationToken);
            }
        }

        private async Task<Queue<ScheduleJobModel>> EnqueueJobsAsync(TTWebContext context,
            CancellationToken cancellationToken)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var jobs = await context.ScheduleJobs
                .Include(j => j.Sender)
                .Include(j => j.Receiver)
                .FilterOpenJobs()
                .ToListAsync(cancellationToken);

            var now = DateTime.UtcNow;
            jobs.ForEach(j => j.Lock(now, _schedulingAppSettings.Job.LockDuration));
            await context.SaveChangesAsync(cancellationToken);

            return new Queue<ScheduleJobModel>(jobs.Select(j => _mapper.Map<ScheduleJobModel>(j)));
        }

        private async Task<ScheduleJobModel> UpdateStatusAsync(TTWebContext context,
            ScheduleJobModel job,
            ProcessingResult<ScheduleJobModel> result,
            CancellationToken cancellationToken)
        {
            if (job == null) throw new ArgumentNullException(nameof(job));
            if (result == null) throw new ArgumentNullException(nameof(result));

            var dbJob = new ScheduleJob
            {
                Id = job.Id,
                Status = result.Succeed ? ProcessingStatus.Completed : ProcessingStatus.Error
            };

            context.ScheduleJobs.Attach(dbJob);
            await context.SaveChangesAsync(cancellationToken);

            return _mapper.Map(dbJob, job);
        }

        private static async Task CreateScheduleJobResultAsync(TTWebContext context,
            ScheduleJobModel job,
            CancellationToken cancellationToken)
        {
            if (job == null) throw new ArgumentNullException(nameof(job));

            var jobResult = new ScheduleJobResult {ScheduleJobId = job.Id};
            await context.ScheduleJobsResults.AddAsync(jobResult, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}