using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Services.Worker;

namespace TTWeb.Worker.ScheduleExecutor
{
    public class ScheduleExecutorWorker : BackgroundService
    {
        private readonly IWorkerClientService _workerClientService;
        private readonly ILogger<ScheduleExecutorWorker> _logger;
        private readonly SchedulingAppSettings _schedulingAppSettings;
        private readonly IFacebookSeleniumService _facebookService;

        public ScheduleExecutorWorker(IWorkerClientService workerClientService,
            ILogger<ScheduleExecutorWorker> logger,
            IOptions<SchedulingAppSettings> schedulingAppSettingsOptions,
            IFacebookSeleniumService facebookService)
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

                var jobs = await _workerClientService.FetchJobsAsync();

                if (jobs.Count == 0)
                {
                    _logger.LogInformation($"No unprocessed job found - takes a short break of {_schedulingAppSettings.Planning.TriggerInterval.TotalMinutes} minutes");
                    await Task.Delay(_schedulingAppSettings.Planning.TriggerInterval, stoppingToken);
                }
                else
                {
                    await DoWorkAsync(jobs);
                    _logger.LogInformation("Unprocessed job found - restart immediately");
                }
            }
        }

        private Task DoWorkAsync(List<BusinessLogic.Models.Entities.ScheduleJobModel> jobs)
        {
            throw new NotImplementedException();
        }
    }
}
