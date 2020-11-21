using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TTWeb.BusinessLogic.Services.Worker;

namespace TTWeb.Worker.SchedulePlanningTrigger
{
    public class Worker : BackgroundService
    {
        private readonly IWorkerService _workerService;
        private readonly ILogger<Worker> _logger;

        public Worker(IWorkerService workerService, ILogger<Worker> logger)
        {
            _workerService = workerService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _workerService.TriggerPlanningAsync();
            }
        }
    }
}
