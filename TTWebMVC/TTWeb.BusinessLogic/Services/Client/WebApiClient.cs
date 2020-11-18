using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TTWeb.BusinessLogic.Models.AppSettings;

namespace TTWeb.BusinessLogic.Services.Client
{
    public class WebApiClient
    {
        private readonly WebApiAppSettings _webApiAppSettings;
        public HttpClient Client { get; set; }

        public WebApiClient(IOptions<HttpClientAppSettings> httpClientAppSettingsOptions, 
            HttpClient client)
        {
            _webApiAppSettings = httpClientAppSettingsOptions.Value.WebApi;
            client.BaseAddress = new Uri(_webApiAppSettings.BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpClientAppSettings.AcceptHeaderDefault));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("planning-trigger-localhost"));
            Client = client;
        }

        public async Task AuthenticateAsync()
        {
            if (!TryGetStoredAccessToken())
                await GetAccessTokenAsync();

            if (IsTokenRefreshRequired())
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
        private bool TryGetStoredAccessToken()
        {
            // TODO: reads local token
            // TODO: validates expiration date
            return false;
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
        /// Returns true, if token has only half of the time left
        /// </summary>
        /// <returns></returns>
        private bool IsTokenRefreshRequired()
        {
            return true;
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
