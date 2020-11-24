using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Models.AppSettings.WebApi;
using TTWeb.BusinessLogic.Services.Client;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public class WorkerClientService : IWorkerClientService
    {
        private readonly WorkerWebApiClient _webApiClient;
        private readonly WebApiAppSettings _webApiAppSettings;

        public WorkerClientService(WorkerWebApiClient webApiClient,
            IOptions<HttpClientAppSettings> httpClientAppSettingsOptions)
        {
            _webApiClient = webApiClient;
            _webApiAppSettings = httpClientAppSettingsOptions.Value.WebApi;
        }

        public async Task TriggerPlanningAsync()
        {
            await _webApiClient.AuthenticateAsync();
            var response = await _webApiClient.PostAsync(_webApiAppSettings.Routes.TriggerPlanning);
            response.EnsureSuccessStatusCode();
        }
    }
}
