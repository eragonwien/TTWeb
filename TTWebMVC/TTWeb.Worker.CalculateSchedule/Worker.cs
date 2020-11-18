using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TTWeb.BusinessLogic.Services.Box;

namespace TTWeb.Worker.SchedulePlanningTrigger
{
    public class Worker : BackgroundService
    {
        private readonly IBoxService _boxService;
        private readonly ILogger<Worker> _logger;

        public Worker(IBoxService boxService, ILogger<Worker> logger)
        {
            _boxService = boxService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _boxService.TriggerPlanningAsync();
            }
        }
    }
}
