using OpenQA.Selenium;

namespace TTWebCommon.Facebook
{
   public interface IFacebookTTWebDriver
   {
      void Login(FacebookServiceParameter parameter);
      void NavigateTo(string url);
      bool TryGetElement(By byPath, out IWebElement webElement);
      bool TryGetElement(IWebElement parent, By byPath, out IWebElement webElement);
   }
}
