using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TTWeb.Worker.Core
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureWorkerLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration((context, configuration) =>
            {
                // TODO: appsettings.json contains only connection-string, loads other settings from database 
                // ref: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2#custom-configuration-provider-1
                configuration
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
            });
        }

        public static IHostBuilder ConfigureWorkerAppConfiguration(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureLogging(c =>
            {
                c.ClearProviders();
                c.AddConsole();
                c.SetMinimumLevel(LogLevel.Debug);
            });
        }
    }
}