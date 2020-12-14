using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                    WorkerClientService.ConfigureServices(context, services);
                    services.AddHostedService<TriggerPlanningWorker>();
                })
                .ConfigureLogging(WorkerClientService.ConfigureLogging);
    }
}
