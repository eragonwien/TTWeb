using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium.Chrome;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Models.AppSettings.Security;
using TTWeb.BusinessLogic.Services;
using TTWeb.Helper.Otp;
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
                        .AddSingleton<IOtpHelperService, OtpHelperService>()
                        .AddHostedService<ScheduleRunnerWorker>();
                });
        }
    }
}