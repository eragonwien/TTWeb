using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Worker.Core.Services;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public class ScheduleRunnerWorker : BackgroundService
    {
        private readonly IWorkerClientService _workerClientService;
        private readonly ILogger<ScheduleRunnerWorker> _logger;
        private readonly SchedulingAppSettings _schedulingAppSettings;
        private readonly IFacebookAutomationService _facebookService;

        private List<ScheduleJobModel> jobs = new List<ScheduleJobModel>();
        private ScheduleJobModel workingJob = null;

        public ScheduleRunnerWorker(IWorkerClientService workerClientService,
            ILogger<ScheduleRunnerWorker> logger,
            IOptions<SchedulingAppSettings> schedulingAppSettingsOptions,
            IFacebookAutomationService facebookService)
        {
            _workerClientService = workerClientService;
            _logger = logger;
            _schedulingAppSettings = schedulingAppSettingsOptions.Value;
            _facebookService = facebookService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}");

                if (jobs.Count == 0)
                    jobs = await _workerClientService.GetJobsAsync();

                if (jobs.Count == 0)
                {
                    _logger.LogInformation($"No unprocessed job found - takes a short break of {_schedulingAppSettings.Planning.TriggerInterval.TotalMinutes} minutes");
                    await Task.Delay(_schedulingAppSettings.Planning.TriggerInterval, stoppingToken);
                }
                else
                {
                    await DoWorkAsync();
                    _logger.LogInformation("Job completed - restart immediately");
                }
            }
        }

        private async Task DoWorkAsync()
        {
            GetJob();
            await _facebookService.ProcessAsync(workingJob);
            workingJob = null;
        }

        private void GetJob()
        {
            if (workingJob != null) return;
            workingJob = jobs.Last();
            jobs.Remove(workingJob);
        }
    }
}