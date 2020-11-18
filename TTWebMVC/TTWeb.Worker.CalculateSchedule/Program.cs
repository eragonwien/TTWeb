using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Services.Box;
using TTWeb.BusinessLogic.Services.Client;

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

                    services.AddHttpClient<WebApiClient>();
                    services.AddSingleton<IBoxService, BoxService>();
                    services.AddHostedService<Worker>();
                })
                .ConfigureLogging(o =>
                {
                    o.ClearProviders();
                    o.AddConsole();
                    o.SetMinimumLevel(LogLevel.Information);
                });
    }
}
