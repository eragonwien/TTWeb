using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TTWeb.BusinessLogic.Models.AppSettings;

namespace TTWeb.BusinessLogic.Services.Box
{
    public class BoxService : IBoxService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BoxService> _logger;
        private readonly SchedulingAppSettings _schedulingAppSettings;

        public BoxService(IHttpClientFactory httpClientFactory,
            ILogger<BoxService> logger,
            IOptions<SchedulingAppSettings> schedulingAppSettingsOptions)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _schedulingAppSettings = schedulingAppSettingsOptions.Value;
        }

        public Task AuthenticateAsync()
        {
            // TODO: Sends clientId and clientSecret to server at POST /account/box-login
            // TODO: receives jwt token from server
            // TODO: stores token locally
            // TODO: retrieves token from storage (skips if already has token)
            // TODO: validates expiration date
            // TODO: refresh token if expiration date almost dues
            throw new System.NotImplementedException();
        }

        public Task TriggerPlanningAsync()
        {
            // TODO: POST at /api/schedules/trigger-planning
            throw new System.NotImplementedException();
        }
    }
}
