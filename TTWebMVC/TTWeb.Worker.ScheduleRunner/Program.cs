using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.Worker.Core;
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
                .ConfigureWorkerAppConfiguration()
                .ConfigureWorkerLogging()
                .ConfigureServices((context, services) =>
                {
                    services
                        .RegisterDbContext(context.Configuration)
                        .RegisterAutoMapper()
                        .AddSingleton<IFacebookAutomationService, FacebookAutomationService>()
                        .AddHostedService<ScheduleRunnerWorker>();
                });
    }
}