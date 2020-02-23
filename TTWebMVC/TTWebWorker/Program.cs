using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TTWebCommon.Facebook;
using TTWebCommon.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace TTWebWorker
{
   class Program
   {
      public static async Task Main(string[] args)
      {
         await CreateHostBuilder(args).Build().RunAsync();
      }

      private static IHostBuilder CreateHostBuilder(string[] args)
      {
         var builder = new HostBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
               config.SetBasePath(Directory.GetCurrentDirectory());
               config.AddJsonFile("appsettings.json", true, true);
               config.AddEnvironmentVariables();
               if (args != null)
               {
                  config.AddCommandLine(args);
               }
            })
            .ConfigureServices((context, services) =>
            {
               services.AddOptions();
               services.Configure<DaemonConfig>(context.Configuration.GetSection("Daemon"));
               services.AddDbContext<TTWebDbContext>(o => o.UseMySQL(context.Configuration.GetConnectionString("TTWeb")));
               services.AddScoped<IFacebookService, FacebookService>();
               services.AddSingleton<IHostedService, DaemonService>();
            })
            .ConfigureLogging((context, logging) =>
            {
               logging.ClearProviders();
               logging.AddConfiguration(context.Configuration.GetSection("Logging"));
               logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
               logging.AddConsole();
            });

         
         return builder;
      }
   }
}
