using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.AppSettings.Token;
using TTWeb.BusinessLogic.Models.AppSettings.WebApi;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Services.Authentication;

namespace TTWeb.BusinessLogic.Services.Client
{
    /// <summary>
    /// Serves the communication between worker client and web api
    /// </summary>
    public class WorkerWebApiClient
    {
        public HttpClient Client { get; set; }

        private readonly AutheticationJsonWebTokenAppSettings _jsonWebTokenAppSettings;
        private readonly WorkerAppSettings _workerAppSettings;
        private readonly WebApiAppSettings _webApiAppSettings;
        private readonly IAuthenticationHelperService _authenticationHelperService;
        private LoginTokenModel _token = new LoginTokenModel();

        public WorkerWebApiClient(IOptions<HttpClientAppSettings> httpClientAppSettingsOptions,
            IOptions<WorkerAppSettings> workerAppSettingsOptions,
            IOptions<AuthenticationAppSettings> authenticationAppSettingsOptions,
            IAuthenticationHelperService authenticationHelperService,
            HttpClient client)
        {
            _workerAppSettings = workerAppSettingsOptions.Value;
            _jsonWebTokenAppSettings = authenticationAppSettingsOptions.Value.JsonWebToken.Merge(_workerAppSettings);
            _webApiAppSettings = httpClientAppSettingsOptions.Value.WebApi;
            _authenticationHelperService = authenticationHelperService;

            client.BaseAddress = new Uri(_webApiAppSettings.BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpClientAppSettings.AcceptHeaderDefault));
            Client = client;
        }

        public async Task AuthenticateAsync()
        {
            if (!IsTokenRefreshRequired(_token.AccessToken, _jsonWebTokenAppSettings.AccessToken.Duration))
                await RequestAccessTokenAsync();

            if (!IsTokenRefreshRequired(_token.RefreshToken, _jsonWebTokenAppSettings.RefreshToken.Duration))
                await RequestRefreshTokenAsync();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.AccessToken.Token);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object requestMessage = null)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            var json = JsonConvert.SerializeObject(requestMessage);
            return await Client.PostAsync(url, new StringContent(json, Encoding.UTF8, HttpClientAppSettings.AcceptHeaderDefault));
        }

        private bool IsTokenRefreshRequired(TokenModel token, TimeSpan tokenMaxDuration)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return !token.IsEmpty
                   && !token.Expired &&
                   !_authenticationHelperService.IsAlmostExpired(token.ExpirationDateUtc, tokenMaxDuration);
        }

        /// <summary>
        /// Gets new access token from server using client id and secret
        /// Throws exception if the authentication fails
        /// </summary>
        /// <returns></returns>
        private async Task RequestAccessTokenAsync()
        {
            _token.Reset();

            var loginModel = new WorkerModel(_workerAppSettings.ClientId, _workerAppSettings.ClientSecret);

            var response = await PostAsync(_webApiAppSettings.Routes.WorkerLogin, loginModel);
            response.EnsureSuccessStatusCode();

            _token = await response.LoadJsonAsync<LoginTokenModel>();
        }

        /// <summary>
        /// Gets new access token from server using refresh token
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task RequestRefreshTokenAsync()
        {
            var response = await PostAsync(_webApiAppSettings.Routes.RefreshToken, _token);

            if (!response.IsSuccessStatusCode)
            {
                await RequestAccessTokenAsync();
                return;
            }

            _token = await response.LoadJsonAsync<LoginTokenModel>();
        }
    }
}
