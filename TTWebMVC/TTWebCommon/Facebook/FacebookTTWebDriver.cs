using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TTWebCommon.Facebook
{
   public class FacebookTTWebDriver : ChromeDriver, IFacebookTTWebDriver
   {
      private const string LoginUrl = "https://www.facebook.com";

      public FacebookTTWebDriver(string driverDir, ChromeOptions options) : base(driverDir, options)
      {

      }

      public void Login(FacebookServiceParameter parameter)
      {
         NavigateTo(LoginUrl);
         var emailInput = FindElementByXPath("//input[@id='email' or @name='email']");
         emailInput.SendKeys(parameter.Email);
         var passwordInput = FindElementByXPath("//input[@id='pass' and @type='password']");
         passwordInput.SendKeys(parameter.Password);
         var submitButton = FindElementByXPath("//label[@id='loginbutton']/input[@data-testid='royal_login_button' and @type='submit']");
         submitButton.Click();
      }

      public void NavigateTo(string url)
      {
         Navigate().GoToUrl(url);
      }

      public bool TryGetElement(By byPath, out IWebElement webElement)
      {
         try
         {
            webElement = FindElement(byPath);
            return true;
         }
         catch (NoSuchElementException)
         {
            webElement = null;
            return false;
         }
      }

      public bool TryGetElement(IWebElement parent, By byPath, out IWebElement webElement)
      {
         try
         {
            webElement = parent.FindElement(byPath);
            return true;
         }
         catch (NoSuchElementException)
         {
            webElement = null;
            return false;
         }
      }
   }
}
