using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TTWeb.Worker.Core
{
    public abstract class BaseWorker : BackgroundService
    {
        protected readonly IServiceScope _scopeFactory;

        protected BaseWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory.CreateScope();
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await DoContinuousWorkAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex}");
                    throw;
                }
            }
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _scopeFactory.Dispose();
        }

        protected abstract Task DoContinuousWorkAsync(CancellationToken cancellationToken);
    }
}
