using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SNGCommon.Authentication;
using TTWebApi.Models;
using TTWebCommon.Models;

namespace TTWebApi
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var host = CreateWebHostBuilder(args).Build();
         try
         {
            InitializeDatabase(host);
         }
         catch (Exception ex)
         {
            throw ex;
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
