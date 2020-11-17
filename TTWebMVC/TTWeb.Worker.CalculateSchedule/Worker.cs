using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TTWeb.Worker.CalculateSchedule
{
    public class Worker : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(IHttpClientFactory clientFactory, ILogger<Worker> logger)
        {
            _httpClientFactory = clientFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await TriggerPlanningAsync();
            }
        }

        private async Task TriggerPlanningAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/aspnet/AspNetCore.Docs/branches");

            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
