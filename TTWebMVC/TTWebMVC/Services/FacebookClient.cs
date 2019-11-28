using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TTWebMVC.Models.SettingModels;

namespace TTWebMVC.Services
{
   public interface IFacebookClient
   {
      string GetLongLivedAccessToken(string accessToken);
   }
   public class FacebookClient : IFacebookClient
   {
      private readonly HttpClient httpClient;
      private readonly ILogger<FacebookClient> log;
      private readonly IOptions<FacebookConfig> config;
      private readonly string AppId;
      private readonly string AppSecret;

      public FacebookClient(HttpClient httpClient, ILogger<FacebookClient> log, IOptions<FacebookConfig> config)
      {
         this.httpClient = httpClient;
         this.log = log;
         this.config = config;
         AppId = config.Value.AppId;
         AppSecret = config.Value.AppSecret;
      }

      public string GetLongLivedAccessToken(string accessToken)
      {
         return accessToken;
      }
   }
}
