using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
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
