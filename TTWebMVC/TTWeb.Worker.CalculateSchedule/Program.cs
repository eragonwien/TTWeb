using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using TTWeb.BusinessLogic.Models.AppSettings;

namespace TTWeb.Worker.CalculateSchedule
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

                    // TODO: adds ttweb web api client
                    services.AddHttpClient();
                    services.AddHostedService<SchedulePlanningTrigger.Worker>();

                })
                .ConfigureLogging(o =>
                {
                    o.ClearProviders();
                    o.AddConsole();
                    o.SetMinimumLevel(LogLevel.Information);
                });
    }
}
