using NHtmlUnit.Html;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebNetCommon.Facebook
{
   public interface IFacebookTTWebDriver
   {
      string Login(string user, string password);
      void Like(FacebookServiceParameter parameter);
      void Comment(FacebookServiceParameter parameter);
      string TestHtml(string targetUrl);
   }
}
