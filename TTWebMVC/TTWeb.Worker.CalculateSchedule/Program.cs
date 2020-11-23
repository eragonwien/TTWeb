using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Services.Authentication;
using TTWeb.BusinessLogic.Services.Client;
using TTWeb.BusinessLogic.Services.Worker;

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
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile($"appsettings.json", optional: true)
                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: false);
                })
                .ConfigureServices((context, services) =>
                {
                    services.Configure<HttpClientAppSettings>(context.Configuration.GetSection(HttpClientAppSettings.Section));
                    services.Configure<WorkerAppSettings>(context.Configuration.GetSection(WorkerAppSettings.Section));
                    services.Configure<AuthenticationAppSettings>(context.Configuration.GetSection(AuthenticationAppSettings.Section));

                    services.AddHttpClient<WorkerWebApiClient>();
                    services.AddSingleton<IAuthenticationHelperService, AuthenticationHelperService>();
                    services.AddSingleton<IWorkerClientService, WorkerClientService>();
                    services.AddHostedService<TriggerPlanningWorker>();
                })
                .ConfigureLogging(o =>
                {
                    o.ClearProviders();
                    o.AddConsole();
                    o.SetMinimumLevel(LogLevel.Information);
                });
    }
}
