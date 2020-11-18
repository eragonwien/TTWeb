using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.AppSettings;

namespace TTWeb.BusinessLogic.Services.Client
{
    public class WebApiClient
    {
        public HttpClient Client { get; set; }

        private readonly AuthenticationJsonWebTokenAppSettings _jsonWebTokenAppSettings;
        private LoginTokenModel _token = new LoginTokenModel();

        public WebApiClient(IOptions<HttpClientAppSettings> httpClientAppSettingsOptions, 
            IOptions<AuthenticationAppSettings> authenticationAppSettingsOptions, 
            HttpClient client)
        {
            var webApiAppSettings = httpClientAppSettingsOptions.Value.WebApi;
            _jsonWebTokenAppSettings = authenticationAppSettingsOptions.Value.JsonWebToken;

            client.BaseAddress = new Uri(webApiAppSettings.BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpClientAppSettings.AcceptHeaderDefault));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("planning-trigger-localhost"));
            Client = client;
        }

        public async Task AuthenticateAsync()
        {
            if (!HasValidStoredToken() || _token.AccessToken.IsAlmostExpired(_jsonWebTokenAppSettings.AccessToken.Duration))
                await GetAccessTokenAsync();

            if (_token.RefreshToken.IsAlmostExpired(_jsonWebTokenAppSettings.RefreshToken.Duration))
                await RefreshTokenAsync();
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object requestMessage = null)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            var json = JsonConvert.SerializeObject(requestMessage);
            return await Client.PostAsync(url, new StringContent(json, Encoding.UTF8, HttpClientAppSettings.AcceptHeaderDefault));
        }

        /// <summary>
        /// Returns true if there is an access token stored locally, which has not expired
        /// </summary>
        /// <returns></returns>
        private bool HasValidStoredToken()
        {
            return !_token.AccessToken.IsEmpty            
                   && !_token.RefreshToken.IsEmpty 
                   && !_token.AccessToken.Expired
                   &&!_token.RefreshToken.Expired;
        }

        /// <summary>
        /// Gets new access token from server using client id and secret
        /// </summary>
        /// <returns></returns>
        private async Task GetAccessTokenAsync()
        {
            // TODO: Sends clientId and clientSecret to server at POST /account/box-login
            // TODO: receives jwt token from server
            // TODO: stores token locally
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets new access token from server using refresh token
        /// </summary>
        /// <returns></returns>
        private async Task RefreshTokenAsync()
        {
            // TODO: Sends access- & refresh token to server at POST /account/refresh-token
            // TODO: receives jwt token from server
            // TODO: stores token locally
            throw new NotImplementedException();
        }
    }
}
