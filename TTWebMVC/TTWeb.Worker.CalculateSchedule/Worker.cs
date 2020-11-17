using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TTWeb.BusinessLogic.Services.Schedule;

namespace TTWeb.Worker.CalculateSchedule
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IScheduleService _scheduleService;

        public Worker(ILogger<Worker> logger, IScheduleService scheduleService)
        {
            _logger = logger;
            _scheduleService = scheduleService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // TODO: Creates stand alone service
                // TODO: Triggers planning
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
