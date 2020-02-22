using NHtmlUnit;
using NHtmlUnit.Html;
using NHtmlUnit.W3C.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TTWebNetCommon.Facebook
{
   public class FacebookTTWebDriver : WebClient, IFacebookTTWebDriver, IDisposable
   {
      private const string LoginUrl = "https://www.facebook.com";

      public FacebookTTWebDriver(BrowserVersion version) : base(version)
      {
         Options.JavaScriptEnabled = true;
         Options.ThrowExceptionOnFailingStatusCode = false;
         Options.ThrowExceptionOnScriptError = false;
      }

      public void Comment(FacebookServiceParameter parameter)
      {
         throw new NotImplementedException();
      }

      private List<HtmlElement> GetCommentableItems(HtmlPage page)
      {
         return GetElementsByXpath(page, "//form[@class='commentable_item']");
      }

      public void Like(FacebookServiceParameter parameter)
      {
         Login(parameter.Email, parameter.Password);
         var page = GetHtmlPage(parameter.TargetUrl);
         var commentables = GetCommentableItems(page) ?? new List<HtmlElement>();
         DoLike(commentables);
      }

      private void DoLike(List<HtmlElement> commentables)
      {
         foreach (var item in commentables)
         {
            if (TryGetElementByXpath(item, ".//a[@data-testid='UFI2ReactionLink' and @aria-pressed='false']", out HtmlElement likeButton))
            {
               likeButton.Click();
               continue;
            }
         }
      }

      public string TestHtml(string targetUrl)
      {
         var page = Navigate(targetUrl);
         return page.AsXml();
      }

      public string Login(string user, string password)
      {
         var page = GetHtmlPage(LoginUrl);
         var emailInput = page.GetHtmlElementById("email");
         emailInput.Type(user);
         var passwordInput = page.GetHtmlElementById("pass");
         passwordInput.Type(password);
         var loginButton = page.GetHtmlElementById("loginbutton").GetFirstByXPath("//input[@type='submit']") as HtmlElement;
         var mainpage = (HtmlPage)loginButton.Click();
         return mainpage.AsXml();
      }

      private bool TryGetElementByXpath(HtmlElement parent, string xpath, out HtmlElement element)
      {
         try
         {
            element = GetElementsByXpath(parent, xpath).FirstOrDefault();
            return element != null;
         }
         catch (Exception)
         {
            element = null;
            return false;
         }
      }

      private List<HtmlElement> GetElementsByXpath(DomNode parent, string xpath)
      {
         var elements = parent.GetByXPath(xpath).Select(e => e as HtmlElement).ToList();
         return elements;
      }

      private HtmlPage Navigate(string url)
      {
         var page = GetPage(url);
         return (HtmlPage)page;
      }

      private HtmlPage CurrentPage()
      {
         return CurrentWindow.EnclosedPage as HtmlPage;
      }

      public void Dispose()
      {
         Close();
      }
   }
}
