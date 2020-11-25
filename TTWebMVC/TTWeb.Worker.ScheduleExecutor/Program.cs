using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Services.Worker;

namespace TTWeb.Worker.ScheduleExecutor
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
                    services.AddHostedService<ScheduleExecutorWorker>();
                })
                .ConfigureLogging(WorkerClientService.ConfigureLogging);
    }
}