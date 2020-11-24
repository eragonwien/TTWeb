using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Services.Worker;

namespace TTWeb.Worker.SchedulePlanningTrigger
{
    public class TriggerPlanningWorker : BackgroundService
    {
        private readonly IWorkerClientService _workerClientService;
        private readonly ILogger<TriggerPlanningWorker> _logger;
        private readonly SchedulingAppSettings _schedulingAppSettings;

        public TriggerPlanningWorker(IWorkerClientService workerClientService,
            ILogger<TriggerPlanningWorker> logger,
            IOptions<SchedulingAppSettings> schedulingAppSettingsOptions)
        {
            _workerClientService = workerClientService;
            _logger = logger;
            _schedulingAppSettings = schedulingAppSettingsOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}");
                await _workerClientService.TriggerPlanningAsync();
                _logger.LogInformation($"Planning triggered successfully at {DateTimeOffset.Now}");
                await Task.Delay(_schedulingAppSettings.Planning.TriggerInterval, stoppingToken);
            }
        }
    }
}
