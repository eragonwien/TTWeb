using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.Worker.Core;

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
                .ConfigureWorkerAppConfiguration()
                .ConfigureWorkerLogging()
                .ConfigureServices((context, services) =>
                {
                    services
                        .RegisterDbContext(context.Configuration)
                        .RegisterAutoMapper()
                        .Configure<SchedulingAppSettings>(context.Configuration.GetSection(SchedulingAppSettings.Section))
                        .AddHostedService<TriggerPlanningWorker>();
                });
    }
}
