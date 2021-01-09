using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.Data.Database;

namespace TTWeb.Worker.Core
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureWorkerLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration((context, configuration) =>
            {
                configuration
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", true)
                    .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true);

                var localConfiguration = configuration.Build();
                configuration.AddDbContextConfiguration(b => TTWebContext.UseDbContext(b, localConfiguration));
            });
        }

        public static IHostBuilder ConfigureWorkerAppConfiguration(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureLogging(c =>
            {
                c.AddConsole();
            });
        }
    }
}