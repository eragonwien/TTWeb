using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;
using TTWeb.Data.Models;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public class ScheduleRunnerWorker : BackgroundService
    {
        private readonly ILogger<ScheduleRunnerWorker> _logger;
        private readonly SchedulingAppSettings _schedulingAppSettings;
        private readonly IFacebookAutomationService _facebookService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

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

                var queue = await EnqueueJobsAsync(cancellationToken);

                while (queue.TryDequeue(out var job) && !cancellationToken.IsCancellationRequested)
                {
                    var result = await _facebookService.ProcessAsync(job, cancellationToken);
                    await UpdateStatusAsync(job, result, cancellationToken);
                }
                   

                await Task.Delay(_schedulingAppSettings.Job.TriggerInterval, cancellationToken);
            }
        }

        private async Task<Queue<ScheduleJobModel>> EnqueueJobsAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TTWebContext>();
            
            var jobs = await  context.ScheduleJobs
                .Include(j => j.Sender)
                .Include(j => j.Receiver)
                .FilterOpenJobs()
                .ToListAsync(cancellationToken);

            var now = DateTime.UtcNow;
            jobs.ForEach(j => j.Lock(now, _schedulingAppSettings.Job.LockDuration));
            await context.SaveChangesAsync(cancellationToken);

            return new Queue<ScheduleJobModel>(jobs.Select(j => _mapper.Map<ScheduleJobModel>(j)));
        }

        private async Task UpdateStatusAsync(ScheduleJobModel job,
            bool succeed,
            CancellationToken cancellationToken)
        {
            if (job == null) throw new ArgumentNullException(nameof(job));
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TTWebContext>();

            context.ScheduleJobs.Attach(new ScheduleJob
            {
                Id = job.Id,
                Status = succeed ? ProcessingStatus.Completed : ProcessingStatus.Error
            });
            await context.SaveChangesAsync(cancellationToken);

            var jobResult = new ScheduleJobResult { ScheduleJobId =  job.Id };
            await context.ScheduleJobsResults.AddAsync(jobResult, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}