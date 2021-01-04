using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Services;
using TTWeb.Worker.Core.Services;

namespace TTWeb.Worker.SchedulePlanningTrigger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(WorkerClientService.ConfigureAppConfiguration)
                .ConfigureServices((context, services) =>
                {
                    services
                        .RegisterDbContext(context.Configuration)
                        .RegisterAutoMapper()
                        .Configure<SchedulingAppSettings>(context.Configuration.GetSection(SchedulingAppSettings.Section))
                        .AddHostedService<TriggerPlanningWorker>();
                })
                .ConfigureLogging(WorkerClientService.ConfigureLogging);
    }
}
