using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models;
using TTWeb.BusinessLogic.Services;
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

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWorkerAppConfiguration()
                .ConfigureWorkerLogging()
                .ConfigureServices((context, services) =>
                {
                    services
                        .RegisterDbContext(context.Configuration)
                        .RegisterAutoMapper()
                        .Configure<AuthenticationAppSettings>(context.Configuration.GetSection(AuthenticationAppSettings.Section))
                        .Configure<SchedulingAppSettings>(context.Configuration.GetSection(SchedulingAppSettings.Section))
                        .Configure<SecurityAppSettings>(context.Configuration.GetSection(SecurityAppSettings.Section))
                        .AddSingleton<IFacebookChromeDriverService, FacebookChromeDriverService>()
                        .AddSingleton<IFacebookAutomationService, FacebookAutomationService>()
                        .AddSingleton<IEncryptionHelper, EncryptionHelper>()
                        .AddSingleton<IHelperService, HelperService>()
                        .AddHostedService<ScheduleRunnerWorker>();
                });
        }
    }
}