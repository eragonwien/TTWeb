using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings.WebApi;
using TTWeb.BusinessLogic.Services.Client;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public class WorkerService : IWorkerService
    {
        private readonly WebApiClient _webApiClient;
        private readonly ILogger<WorkerService> _logger;
        private readonly WebApiAppSettings _webApiAppSettings;

        public WorkerService(WebApiClient webApiClient,
            ILogger<WorkerService> logger,
            IOptions<WebApiAppSettings> webApiAppSettingsOptions)
        {
            _webApiClient = webApiClient;
            _logger = logger;
            _webApiAppSettings = webApiAppSettingsOptions.Value;
        }

        public async Task TriggerPlanningAsync()
        {
            await _webApiClient.AuthenticateAsync();
            var response = await _webApiClient.PostAsync(_webApiAppSettings.Routes.TriggerPlanning);
            response.EnsureSuccessStatusCode();
        }
    }
}
