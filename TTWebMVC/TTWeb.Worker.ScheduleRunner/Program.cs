using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Services.Worker;
using TTWeb.Worker.ScheduleRunner.Services;

namespace TTWeb.Worker.ScheduleRunner
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
                    services.AddSingleton<IFacebookAutomationService, FacebookAutomationService>();
                    services.AddHostedService<ScheduleRunnerWorker>();
                })
                .ConfigureLogging(WorkerClientService.ConfigureLogging);
    }
}