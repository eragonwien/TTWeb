using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TTWebMVC.Models.Facebook;
using TTWebMVC.Models.SettingModels;

namespace TTWebMVC.Services
{
   public interface IFacebookClient
   {
      Task<FacebookTokenInfo> GetLongLivedAccessToken(string accessToken);
      Task<List<FacebookPageInfo>> GetAllPages(string accessToken);
   }
   public class FacebookClient : IFacebookClient
   {
      private readonly HttpClient httpClient;
      private readonly ILogger<FacebookClient> log;
      private readonly IOptions<FacebookConfig> config;
      private readonly string AppId;
      private readonly string AppSecret;

      private const string version = "v5.0";
      private const string baseEndPoint = "https://graph.facebook.com";

      public FacebookClient(HttpClient httpClient, ILogger<FacebookClient> log, IOptions<FacebookConfig> config)
      {
         this.httpClient = httpClient;
         this.log = log;
         this.config = config;
         AppId = config.Value.AppId;
         AppSecret = config.Value.AppSecret;
      }

      private string GetCombinedUrl(string action, Dictionary<string, string> parameters)
      {
         parameters["client_id"] = AppId;
         parameters["client_secret"] = AppSecret;
         string url = $"{ baseEndPoint }/{ version }/{ action }";
         string queryParamStr = string.Join("&", parameters.Select(p => string.Format("{0}={1}", p.Key, p.Value)));
         if (!string.IsNullOrEmpty(queryParamStr))
         {
            url += "?" + queryParamStr;
         }
         return url;
      }

      public async Task<FacebookTokenInfo> GetLongLivedAccessToken(string accessToken)
      {
         var parameters = new Dictionary<string, string>
         {
            { "grant_type", "fb_exchange_token" },
            { "fb_exchange_token", accessToken }
         };
         var uri = GetCombinedUrl("oauth/access_token", parameters);
         var response = await httpClient.GetAsync(uri);
         response.EnsureSuccessStatusCode();
         string responseBody = await response.Content.ReadAsStringAsync();
         FacebookAccessTokenResponse facebookResponse = JsonConvert.DeserializeObject<FacebookAccessTokenResponse>(responseBody);
         return FacebookTokenInfo.FromResponse(facebookResponse);
      }

      public async Task<List<FacebookPageInfo>> GetAllPages(string accessToken)
      {
         var parameters = new Dictionary<string, string>();
         parameters = AddAccessToken(accessToken, parameters);
         var uri = GetCombinedUrl("me/accounts", parameters);
         var response = await httpClient.GetAsync(uri);
         response.EnsureSuccessStatusCode();
         string responseBody = await response.Content.ReadAsStringAsync();
         var facebookResponse = JsonConvert.DeserializeObject<FacebookAccountsResponse>(responseBody);
         return FacebookPageInfo.FromResponse(facebookResponse);
      }

      private Dictionary<string, string> AddAccessToken(string accessToken, Dictionary<string, string> parameters)
      {
         parameters.Add("access_token", accessToken);
         return parameters;
      }
   }
}
