﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace TTWebInterface
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
         try
         {
            logger.Debug("init main");
            CreateWebHostBuilder(args).Build().Run();
            logger.Debug("done without error");
         }
         catch (Exception exception)
         {
            logger.Fatal(exception, "Stopped program because of exception");
            throw;
         }
         finally
         {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            NLog.LogManager.Shutdown();
         }
      }

      public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>()
               .ConfigureLogging(l =>
               {
                  l.ClearProviders();
                  l.SetMinimumLevel(LogLevel.Trace);
               })
               .UseNLog();
   }
}
