using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using SNGCommon.Common;
using System;

namespace TTWebMVC
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var logger = NLogBuilder.ConfigureNLog(Settings.NlogConfigFileName).GetCurrentClassLogger();
         try
         {
            logger.Info("Program's Initialization starts");
            var host = CreateWebHostBuilder(args).Build();
            host.Run();
            logger.Info("Program's Initialization completed without error");
         }
         catch (Exception ex)
         {
            logger.Error(ex, "Program stopped due to exception");
            throw;
         }
         finally
         {
            NLog.LogManager.Shutdown();
         }
      }

      public static IWebHostBuilder CreateWebHostBuilder(string[] args)
      {
         return WebHost
            .CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(logging =>
            {
               logging.ClearProviders();
               logging.SetMinimumLevel(LogLevel.Debug);
            })
            .UseNLog();
      }
   }
}
