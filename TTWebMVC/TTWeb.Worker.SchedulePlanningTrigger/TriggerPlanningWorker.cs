using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Database;
using TTWeb.Data.Extensions;

namespace TTWeb.Worker.SchedulePlanningTrigger
{
    public class TriggerPlanningWorker : BackgroundService
    {
        private readonly ILogger<TriggerPlanningWorker> _logger;
        private readonly SchedulingAppSettings settings;
        private readonly IServiceScopeFactory _scopeFactory;

        public TriggerPlanningWorker(ILogger<TriggerPlanningWorker> logger,
            IOptions<SchedulingAppSettings> schedulingAppSettingsOptions, 
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            settings = schedulingAppSettingsOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}");

                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TTWebContext>();

                    var jobs = await  context.ScheduleJobs
                        .Include(j => j.Sender)
                        .Include(j => j.Receiver)
                        .FilterOpenJobs()
                        .ToListAsync(cancellationToken);

                    var now = DateTime.UtcNow;
                    jobs.ForEach(j => j.Lock(now, settings.Job.LockDuration));
                    await context.SaveChangesAsync(cancellationToken);
                }

                _logger.LogInformation($"Schedules triggered successfully at {DateTimeOffset.Now}");
                await Task.Delay(settings.Planning.TriggerInterval, cancellationToken);
            }
        }
    }
}
