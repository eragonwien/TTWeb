using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings;

namespace TTWeb.Worker.CalculateSchedule
{
    public class Worker : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<Worker> _logger;
        private readonly WebApiAppSettings _webApiAppSettings;

        public Worker(IHttpClientFactory clientFactory,
            ILogger<Worker> logger,
            IOptions<WebApiAppSettings> webApiAppSettingsOption)
        {
            _httpClientFactory = clientFactory;
            _logger = logger;
            _webApiAppSettings = webApiAppSettingsOption.Value;
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
            var request = new HttpRequestMessage(HttpMethod.Get,
                _webApiAppSettings.GetRoute(_webApiAppSettings.Routes.TriggerPlanning));

            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
