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
using TTWeb.BusinessLogic.Services.Authentication;

namespace TTWeb.BusinessLogic.Services.Client
{
    public class WebApiClient
    {
        public HttpClient Client { get; set; }

        private readonly AuthenticationJsonWebTokenAppSettings _jsonWebTokenAppSettings;
        private readonly BoxAppSettings _boxAppSettingsOptions;
        private readonly WebApiAppSettings _webApiAppSettings;
        private readonly IAuthenticationHelperService _authenticationHelperService;
        private LoginTokenModel _token = new LoginTokenModel();

        public WebApiClient(IOptions<HttpClientAppSettings> httpClientAppSettingsOptions, 
            IOptions<AuthenticationAppSettings> authenticationAppSettingsOptions, 
            IOptions<BoxAppSettings> boxAppSettingsOptions, 
            IOptions<WebApiAppSettings> webApiAppSettingsOptions, 
            IAuthenticationHelperService authenticationHelperService,
            HttpClient client)
        {
            var webApiAppSettings = httpClientAppSettingsOptions.Value.WebApi;
            _jsonWebTokenAppSettings = authenticationAppSettingsOptions.Value.JsonWebToken;
            _boxAppSettingsOptions = boxAppSettingsOptions.Value;
            _webApiAppSettings = webApiAppSettingsOptions.Value;
            _authenticationHelperService = authenticationHelperService;

            client.BaseAddress = new Uri(webApiAppSettings.BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpClientAppSettings.AcceptHeaderDefault));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("planning-trigger-localhost"));
            Client = client;
        }

        public async Task AuthenticateAsync()
        {
            if (!IsTokenRefreshRequired(_token.AccessToken, _jsonWebTokenAppSettings.AccessToken.Duration)) 
                await GetAccessTokenAsync();

            if (!IsTokenRefreshRequired(_token.RefreshToken, _jsonWebTokenAppSettings.RefreshToken.Duration))
                await RefreshTokenAsync();
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
        private async Task GetAccessTokenAsync()
        {
            _token.Reset();

            var loginModel = new BoxLoginModel(_boxAppSettingsOptions.ClientId, _boxAppSettingsOptions.ClientSecret);
            var response = await PostAsync(_webApiAppSettings.Routes.BoxLogin, loginModel);
            response.EnsureSuccessStatusCode();

            _token = await response.LoadJsonAsync<LoginTokenModel>();
        }

        /// <summary>
        /// Gets new access token from server using refresh token
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task RefreshTokenAsync()
        {
            var response = await PostAsync(_webApiAppSettings.Routes.RefreshToken, _token);

            if (!response.IsSuccessStatusCode)
            {
                await GetAccessTokenAsync();
                return;
            }

            _token = await response.LoadJsonAsync<LoginTokenModel>();
        }
    }
}
