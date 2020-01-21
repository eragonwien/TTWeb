using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTWebAuto.Models;

namespace TTWebAuto.Extensions
{
   public static class ChromeDriverExtensions
   {
      private const string LoginUrl = "https://www.facebook.com";

      public static void Comment(this ChromeDriver browser, IWebElement entry, string message)
      {
         // clicks comment button
         var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
         var commentButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(".//a[@testid='UFI2CommentLink']")));
         browser.ExecuteScript("arguments[0].click();", commentButton);

         // writes comment
         var inputBox = entry.FindElement(By.XPath(".//div[@data-testid='UFI2ComposerInput/comment:rich-input']"));
         inputBox.SendKeys(message);
         inputBox.SendKeys(Keys.Enter);
      }

      public static IWebElement GetCommentableEntry(this ChromeDriver browser)
      {
         var entry = browser.GetElement(By.XPath("//form[@class='commentable_item']"));
         return entry;
      }

      public static void Like(this ChromeDriver browser, IWebElement entry)
      {
         var likeButton = entry.FindElement(By.XPath(".//a[@data-testid='UFI2ReactionLink']"));
         if (bool.TryParse(likeButton.GetAttribute("aria-pressed"), out bool liked) && !liked)
         {
            likeButton.Click();
         }
      }

      public static void Login(this ChromeDriver browser, string email, string password)
      {
         browser.Navigate().GoToUrl(LoginUrl);
         var emailInput = browser.FindElementByXPath("//input[@id='email' or @name='email']");
         emailInput.SendKeys(email);
         var passwordInput = browser.FindElementByXPath("//input[@id='pass' and @type='password']");
         passwordInput.SendKeys(password);
         var submitButton = browser.FindElementByXPath("//label[@id='loginbutton']/input[@data-testid='royal_login_button' and @type='submit']");
         submitButton.Click();
      }

      public static void WaitUntilPageLoaded(this ChromeDriver browser)
      {
         var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
         wait.Until(driver1 => ((IJavaScriptExecutor)browser).ExecuteScript("return document.readyState").Equals("complete"));
      }

      private static void WaitFor(this ChromeDriver browser, int seconds)
      {
         browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
      }

      private static void Debug(this ChromeDriver browser)
      {
         var url = browser.Url;
         var html = browser.PageSource;
      }

      private static bool Exist(this ChromeDriver browser, By byPath)
      {
         try
         {
            browser.FindElement(byPath);
            return true;
         }
         catch (NoSuchElementException)
         {
            return false;
         }
      }

      private static IWebElement GetElement(this ChromeDriver browser, By byPath)
      {
         try
         {
            return browser.FindElement(byPath);
         }
         catch (NoSuchElementException)
         {
            return null;
         }
      }

      public static void NavigateTo(this ChromeDriver browser, string url)
      {
         browser.Navigate().GoToUrl(url);
      }

      public static void ClickProfile(this ChromeDriver browser)
      {
         var userNav = browser.FindElement(By.XPath("//div[@id='userNav']"));
         var profileLink = userNav.FindElement(By.XPath("//a[@class='_5afe']"));
         profileLink.Click();
         browser.WaitFor(1);
      }

      public static void ClickProfileFriendTab(this ChromeDriver browser)
      {
         var timelineTab = browser.FindElement(By.XPath("//div[@id='fbTimelineHeadline']"));
         var friendListTab = timelineTab.FindElement(By.XPath(".//a[@data-tab-key='friends']"));
         friendListTab.Click();
         browser.WaitFor(1);
      }

      public static void WriteProfileFriendTabSearchInput(this ChromeDriver browser, string inputText)
      {
         var timeLineTab = browser.FindElement(By.XPath("//div[@data-referrer='timeline_collections_section_title']"));
         var searchInput = timeLineTab.FindElement(By.XPath("//span[@class='uiSearchInput textInput']/span/input[@type='text' and @class='inputtext']"));
         searchInput.Click();
         searchInput.SendKeys(inputText);
         browser.WaitFor(1);
      }

      public static List<string> GetFriendList(this ChromeDriver browser)
      {
         browser.ClickProfile();
         browser.ClickProfileFriendTab();

         var friendEntries = browser.FindElements(By.XPath("//div[@data-testid='friend_list_item']"));
         var friendList = new List<string>();
         foreach (var entry in friendEntries)
         {
            string link = entry.FindElement(By.XPath(".//a")).GetAttribute("href");
            link = link.Substring(0, link.IndexOf('?'));
            if (!link.Contains(".php") && !link.Contains("/0x"))
            {
               friendList.Add(link);
            }
         }
         return friendList;
      }

      public static void ForceClick(this ChromeDriver browser, IWebElement webElement)
      {
         browser.ExecuteScript("arguments[0].click();", webElement);
      }

      public static void RefocusBody(this ChromeDriver browser)
      {
         var body = browser.FindElement(By.XPath("//body"));
         body.Click();
      }

      public static List<FacebookFriends> SearchFriend(this ChromeDriver browser, string searchText)
      {
         browser.ClickProfile();
         browser.ClickProfileFriendTab();
         browser.WriteProfileFriendTabSearchInput(searchText);
         return browser.ReadProfileFriendTabSearchResults();
      }

      public static List<FacebookFriends> ReadProfileFriendTabSearchResults(this ChromeDriver browser)
      {
         var friendEntries = browser.FindElements(By.XPath("//li[@class='fbProfileBrowserListItem']"));
         var friendList = new List<FacebookFriends>();
         foreach (var entry in friendEntries)
         {
            var ele = entry.FindElement(By.XPath(".//a[@class='name']"));
            if (!ele.GetAttribute("href").Contains("/0x"))
            {
               friendList.Add(new FacebookFriends
               {
                  Link = ele.GetAttribute("href"),
                  Name = ele.Text
               });
            }
         }
         return friendList;
      }
   }
}
