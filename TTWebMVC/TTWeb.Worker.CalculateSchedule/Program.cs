using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.AppSettings;
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
                    config
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", false)
                        .AddJsonFile($"appsettings.{ context.HostingEnvironment.EnvironmentName }.json", false);
                })
                .ConfigureServices((context, services) =>
                {
                    services.Configure<HttpClientAppSettings>(context.Configuration.GetSection(HttpClientAppSettings.Section));
                    services.Configure<WorkerAppSettings>(context.Configuration.GetSection(WorkerAppSettings.Section));

                    services.AddHttpClient<WebApiClient>();
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
