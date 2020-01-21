using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TTWebAuto.Extensions;
using TTWebAuto.Models;

namespace TTWebAuto.Services
{
   public interface IFacebookService
   {
      void Comment();
      void Like(FacebookCredentials credentials, string targetUrl);
      void Like(string email, string password, string targetUrl);
      void GetFriendList();
      void SearchFriends();
   }

   public class FacebookService : IFacebookService
   {
      private readonly ChromeOptions BrowserOptions = new ChromeOptions();
      private readonly string DriverDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

      private readonly string BrowserArgumentHeadless = "headless";
      private readonly string BrowserArgumentDisableExtensions = "--disable-extensions";
      private readonly string BrowserArgumentDisableNotifications = "--disable-notifications";
      private readonly string BrowserArgumentDisableCache = "--disable-application-cache";

      public FacebookService()
      {
         //BrowserOptions.AddArgument(BrowserArgumentHeadless);
         BrowserOptions.AddArgument(BrowserArgumentDisableExtensions);
         BrowserOptions.AddArgument(BrowserArgumentDisableNotifications);
         BrowserOptions.AddArgument(BrowserArgumentDisableCache);
         BrowserOptions.UnhandledPromptBehavior = UnhandledPromptBehavior.Dismiss;
      }

      public void Comment()
      {
         throw new NotImplementedException();
      }

      public void GetFriendList()
      {
         throw new NotImplementedException();
      }

      public void Like(string email, string password, string targetUrl)
      {
         Like(new FacebookCredentials(email, password), targetUrl);
      }

      public void Like(FacebookCredentials credentials, string targetUrl)
      {
         using (var browser = GetBrowser())
         {
            browser.Login(credentials.Email, credentials.Password);
            browser.NavigateTo(targetUrl);
            var entry = browser.GetCommentableEntry();
            if (entry != null)
            {
               browser.Like(entry);
            }
         }
      }

      public void SearchFriends()
      {
         throw new NotImplementedException();
      }

      private ChromeDriver GetBrowser()
      {
         return new ChromeDriver(DriverDir, BrowserOptions);
      }
   }
}
