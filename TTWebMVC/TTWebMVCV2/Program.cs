using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace TTWebMVCV2
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
            host.Run();
         }
         catch (Exception ex)
         {
            log.Error("Error initializing program: {0} - {1}", ex.Message, ex.StackTrace);
         }
      }

      public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                 webBuilder.UseStartup<Startup>();
              });
   }
}
