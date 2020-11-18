using System.Net.Http;
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
    }
}
