using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace TTWeb.Worker.Core
{
    public abstract class BaseWorker : BackgroundService
    {
        protected readonly IServiceScope _scope;

        protected BaseWorker(IServiceScopeFactory scopeFactory)
        {
            _scope = scopeFactory.CreateScope();
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await DoContinuousWorkAsync(cancellationToken);
            }
        }

        protected TDbContext GetRequiredService<TDbContext>() where TDbContext : DbContext
        {
            return _scope.ServiceProvider.GetRequiredService<TDbContext>();
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _scope.Dispose();
        }

        protected abstract Task DoContinuousWorkAsync(CancellationToken cancellationToken);
    }
}
