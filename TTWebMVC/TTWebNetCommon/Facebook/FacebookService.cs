using NHtmlUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;
using TTWebNetCommon.Models;

namespace TTWebNetCommon.Facebook
{
   public interface IFacebookService
   {
      ProcessingResult Execute(FacebookServiceParameter parameter);
   }
   public class FacebookService : IFacebookService
   {
      private FacebookTTWebDriver webClient;

      public ProcessingResult Execute(FacebookServiceParameter parameter)
      {
         var result = new ProcessingResult();
         try
         {
            using (webClient = new FacebookTTWebDriver(BrowserVersion.CHROME))
            {
               switch (parameter.ActionType)
               {
                  case FacebookServiceActionType.LOGIN:
                     result.Result = webClient.Login(parameter.Email, parameter.Password);
                     break;
                  case FacebookServiceActionType.LIKE:
                     webClient.Like(parameter);
                     break;
                  case FacebookServiceActionType.COMMENT:
                     break;
                  case FacebookServiceActionType.TEST_HTML:
                     result.Result = webClient.TestHtml(parameter.TargetUrl);
                     break;
                  default:
                     break;
               }
            }
         }
         catch (Exception ex)
         {
            result.Exception = ex;
         }
         return result;
      }
   }
}
