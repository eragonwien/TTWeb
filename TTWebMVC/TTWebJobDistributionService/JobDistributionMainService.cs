using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace TTWebJobDistributionService
{
    public class JobDistributionMainService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new System.NotImplementedException();
        }
    }
}