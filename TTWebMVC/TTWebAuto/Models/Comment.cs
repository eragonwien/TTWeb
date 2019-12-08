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

namespace TTWebAuto.Models
{
   public class Comment
   {
      [Required]
      [DataType(DataType.EmailAddress)]
      public string Email { get; set; }
      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }
      public string Text { get; set; }
      public string Target { get; set; }

      private const string loginUrl = "https://www.facebook.com/login.php?login_attempt=1&lwv=110";

      public void Execute()
      {
         var chromeOptions = new ChromeOptions();
         chromeOptions.AddArgument("headless");
         var driverDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
         using (var browser = new ChromeDriver(driverDir, chromeOptions))
         {
            browser.Navigate().GoToUrl(loginUrl);
            Login(browser);
            WaitUntilPageLoaded(browser);
            browser.Navigate().GoToUrl("https://www.facebook.com/kim.truong.90260403");
            WaitUntilPageLoaded(browser);
            var entry = browser.FindElementByXPath("//form[@class='commentable_item']");
            //LikeEntry(browser, entry);
            //WaitFor(browser, 3);
            CommentEntry(browser, entry);
         }
      }

      private void CommentEntry(ChromeDriver browser, IWebElement entry)
      {
         // clicks comment button
         var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
         var commentButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(".//a[@testid='UFI2CommentLink']")));
         browser.ExecuteScript("arguments[0].click();", commentButton);

         // writes comment
         var inputBox = entry.FindElement(By.XPath(".//div[@data-testid='UFI2ComposerInput/comment:rich-input']"));
         inputBox.SendKeys(":O");
         inputBox.SendKeys(Keys.Enter);
      }

      private void LikeEntry(ChromeDriver browser, IWebElement entry)
      {
         var likeButton = entry.FindElement(By.XPath(".//a[@data-testid='UFI2ReactionLink']"));
         if (bool.TryParse(likeButton.GetAttribute("aria-pressed"), out bool liked) && !liked)
         {
            likeButton.Click();
         }
      }

      private void Login(ChromeDriver browser)
      {
         var emailInput = browser.FindElementByXPath("//input[@id='email' or @name='email']");
         emailInput.SendKeys(Email);
         var passwordInput = browser.FindElementByXPath("//input[@id='pass' and @type='password']");
         passwordInput.SendKeys(Password);
         var submitButton = browser.FindElementByXPath("//button[@id='loginbutton' and @type='submit']");
         submitButton.Click();
      }

      private void CheckPoint(ChromeDriver browser)
      {
         var newUrl = browser.Url;
         var html = browser.PageSource;
         var checkpointsubmitButton = browser.FindElementById("checkpointSubmitButton");
         checkpointsubmitButton.Click();
      }

      private void WaitUntilPageLoaded(ChromeDriver browser)
      {
         var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
         wait.Until(driver1 => ((IJavaScriptExecutor)browser).ExecuteScript("return document.readyState").Equals("complete"));
      }

      private void WaitFor(ChromeDriver browser, int seconds)
      {
         browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);

      }
   }
}
