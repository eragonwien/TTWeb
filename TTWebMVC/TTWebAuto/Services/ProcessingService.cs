using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TTWebAuto.Services
{
   public class ProcessingService : BackgroundService
   {
      private readonly IFacebookService facebookService;

      public ProcessingService(IFacebookService facebookService)
      {
         this.facebookService = facebookService;
      }

      protected override async Task ExecuteAsync(CancellationToken stoppingToken)
      {
         while (!stoppingToken.IsCancellationRequested)
         {
            facebookService.Like("eragonwien@gmail.com", "Hmv8NPi8ZormkFQBI0H5", "https://www.facebook.com/kim.truong.90260403");
            await Task.Delay(30000, stoppingToken);
         }
      }
   }
}
