using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebWorker
{
   public class DaemonService : IHostedService, IDisposable
   {
      private readonly ILogger log;
      private readonly IOptions<DaemonConfig> config;
      private readonly TTWebDbContext db;
      private Task executingTask;
      private readonly CancellationTokenSource stoppingCts = new CancellationTokenSource();

      private const int ShortWaitTimeSecond = 5;
      private const int LongWaitTimeSecond = 30;


      public DaemonService(ILogger<DaemonService> log, IOptions<DaemonConfig> config, TTWebDbContext db)
      {
         this.log = log;
         this.config = config;
         this.db = db;
      }

      public Task StartAsync(CancellationToken cancellationToken)
      {
         log.LogInformation("Starting daemon " + config.Value.Name);
         executingTask = ExecuteAsync(stoppingCts.Token);

         if (executingTask.IsCompleted)
         {
            return executingTask;
         }

         return Task.CompletedTask;
      }

      public async Task StopAsync(CancellationToken cancellationToken)
      {
         if (executingTask == null)
         {
            return;
         }

         try
         {
            log.LogInformation("Stopping daemon " + config.Value.Name);
            stoppingCts.Cancel();
         }
         finally
         {
            await Task.WhenAny(executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
         }
      }

      protected async Task ExecuteAsync(CancellationToken cancellationToken)
      {
         while (!cancellationToken.IsCancellationRequested)
         {
            var job = await GetJob();
            log.LogInformation("Job Name={0}, Type={1}", job.Name, job.Type.Name);
            await Task.Delay(TimeSpan.FromSeconds(job != null ? ShortWaitTimeSecond : LongWaitTimeSecond), cancellationToken);
         }
      }

      private async Task<ScheduleJob> GetJob()
      {
         return await db.ScheduleJobSet
            .Include(j => j.AppUser)
            .Include(j => j.Parameters).ThenInclude(p => p.Type)
            .Include(j => j.Type)
            .OrderBy(j => j.Id)
            .FirstOrDefaultAsync();
      }

      public void Dispose()
      {
         log.LogInformation("Disposing ...");
      }
   }
}