using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Services.Client;

namespace TTWeb.BusinessLogic.Services.Box
{
    public class BoxService : IBoxService
    {
        private readonly WebApiClient _webApiClient;
        private readonly ILogger<BoxService> _logger;
        private readonly WebApiAppSettings _webApiAppSettings;

        public BoxService(WebApiClient webApiClient,
            ILogger<BoxService> logger,
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
