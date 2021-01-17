using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Services;
using TTWeb.Data.Database;
using TTWeb.Web.Api.Extensions;

namespace TTWeb.Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var log = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                log.Info("Initialize Program");
                var host = CreateHostBuilder(args).Build();
                host
                    .MigrateDatabase<TTWebContext>()
                    .Run();
            }
            catch (Exception ex)
            {
                log.Error("Error initializing program: {0} - {1}", ex.Message, ex.StackTrace);
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    var localConfiguration = configuration.Build();
                    configuration.AddDbContextConfiguration(b => TTWebContext.UseDbContext(b, localConfiguration));
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
        }
    }
}