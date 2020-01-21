using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TTWebAuto.Extensions;

namespace TTWebAuto.Models
{
   public class FacebookModel
   {
      [Required]
      [DataType(DataType.EmailAddress)]
      public string Email { get; set; }
      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }
      public string Text { get; set; }
      public string Target { get; set; }
      private readonly ChromeOptions BrowserOptions = new ChromeOptions();
      private readonly string DriverDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

      private readonly string BrowserArgumentHeadless = "headless";
      private readonly string BrowserArgumentDisableExtensions = "--disable-extensions";
      private readonly string BrowserArgumentDisableNotifications = "--disable-notifications";
      private readonly string BrowserArgumentDisableCache = "--disable-application-cache";

      public FacebookModel()
      {
         //BrowserOptions.AddArgument(BrowserArgumentHeadless);

         BrowserOptions.AddArgument(BrowserArgumentDisableExtensions);
         BrowserOptions.AddArgument(BrowserArgumentDisableNotifications);
         BrowserOptions.AddArgument(BrowserArgumentDisableCache);

         BrowserOptions.UnhandledPromptBehavior = UnhandledPromptBehavior.Dismiss;
      }

      public void Comment()
      {
         using (var browser = new ChromeDriver(DriverDir, BrowserOptions))
         {
            browser.Login(Email, Password);
            browser.NavigateTo(Target);
            var entry = browser.GetCommentableEntry();
            if (entry != null)
            {
               browser.Like(entry);
               browser.Comment(entry, Text);
            }
         }
      }

      public List<FacebookFriends> SearchFriend()
      {
         using (var browser = new ChromeDriver(DriverDir, BrowserOptions))
         {
            browser.Login(Email, Password);
            browser.RefocusBody();
            return browser.SearchFriend(Text);
         }
      }

      public List<string> FriendList()
      {
         using (var browser = new ChromeDriver(DriverDir, BrowserOptions))
         {
            browser.Login(Email, Password);
            browser.RefocusBody();
            return browser.GetFriendList();
         }
      }
   }
}
