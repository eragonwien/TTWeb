using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.AppSettings.Scheduling;
using TTWeb.BusinessLogic.Models.AppSettings.WebApi;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Services;

namespace TTWeb.Worker.Core.Services
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

        public async Task<int> TriggerPlanningAsync()
        {
            await _webApiClient.AuthenticateAsync();
            var response = await _webApiClient.PostAsync(_webApiAppSettings.Routes.TriggerPlanning);
            response.EnsureSuccessStatusCode();
            return Convert.ToInt32(await response.Content.ReadAsStringAsync());
        }

        public static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder configuration)
        {
            configuration
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: false);
        }

        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.Configure<HttpClientAppSettings>(context.Configuration.GetSection(HttpClientAppSettings.Section));
            services.Configure<WorkerAppSettings>(context.Configuration.GetSection(WorkerAppSettings.Section));
            services.Configure<AuthenticationAppSettings>(context.Configuration.GetSection(AuthenticationAppSettings.Section));
            services.Configure<SchedulingAppSettings>(context.Configuration.GetSection(SchedulingAppSettings.Section));

            services.AddHttpClient<WorkerWebApiClient>();
            services.AddSingleton<IAuthenticationHelperService, AuthenticationHelperService>();
            services.AddSingleton<IWorkerClientService, WorkerClientService>();
        }

        public static void ConfigureLogging(ILoggingBuilder logger)
        {
            logger.ClearProviders();
            logger.AddConsole();
            logger.SetMinimumLevel(LogLevel.Information);
        }

        public async Task<List<ScheduleJobModel>> GetJobsAsync()
        {
            await _webApiClient.AuthenticateAsync();
            var response = await _webApiClient.PostAsync(_webApiAppSettings.Routes.GetJobs);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ScheduleJobModel>>(jsonString);
        }
    }
}