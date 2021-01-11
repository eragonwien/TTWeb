using OpenQA.Selenium;
using System;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public interface IFacebookChromeDriverService
    {
        void Launch();
        void Close();
        void OpenStartPage();
        void Login(FacebookUserModel sender);

        void NavigateTo(string url);
        void WriteInput(By by, string inputValue);
        void AcceptCookieAgreement();
        void GetPostings();
        void Like();
        void WaitUntil(Func<IWebDriver, IWebElement> waitCondition);
        void WaitUntilBodyVisible();
        bool TryFindElement(By by, out IWebElement element);
        void ByPassTwoFactorAuthentication(FacebookUserModel sender);
    }
}
