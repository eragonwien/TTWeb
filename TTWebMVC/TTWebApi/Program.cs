using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using SNGCommon.Authentication;
using System;
using TTWebApi.Models;
using TTWebCommon.Models;

namespace TTWebApi
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var log = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
         var host = CreateWebHostBuilder(args).Build();
         try
         {
            log.Info("Initialize Program");
            InitializeDatabase(host);
         }
         catch (Exception ex)
         {
            log.Error("Error initializing program: {0} - {1}", ex.Message, ex.StackTrace);
         }
         host.Run();
      }

      public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>();

      private static void InitializeDatabase(IWebHost host)
      {
         using (var scope = host.Services.CreateScope())
         {
            var dbContext = scope.ServiceProvider.GetRequiredService<TTWebDbContext>();
            var authService = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
            DatabaseInitializer.Initialize(dbContext, authService);
         }
      }
   }
}
