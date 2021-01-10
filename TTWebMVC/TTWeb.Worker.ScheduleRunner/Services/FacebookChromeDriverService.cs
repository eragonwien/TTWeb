using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Worker.ScheduleRunner.Extensions;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public class FacebookChromeDriverService : IFacebookChromeDriverService
    {
        private readonly IHostEnvironment _environment;
        private readonly AuthenticationProvidersFacebookAppSettings _facebookSettings;
        private ChromeDriver driver;

        private static readonly TimeSpan maxWaitingTime = TimeSpan.FromSeconds(20);

        public FacebookChromeDriverService(IHostEnvironment environment,
            IOptions<AuthenticationAppSettings> authenticationAppSettingsOptions)
        {
            _environment = environment;
            _facebookSettings = authenticationAppSettingsOptions.Value.Providers.Facebook;
        }

        public void AcceptCookieAgreement()
        {
            if (TryFindElement(By.Id("accept-cookie-banner-label"), out var element) && element.IsVisible())
                element.Click();
        }

        public void Close()
        {
            driver?.Close();
        }

        public void GetPostings()
        {
            throw new NotImplementedException();
        }

        public void Launch()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-notifications");

            if (_environment.IsDevelopment())
                options.AddArgument("--start-maximized");

            driver = new ChromeDriver(options);
        }

        public void Like()
        {
            throw new NotImplementedException();
        }

        public void Login(ScheduleFacebookUserModel sender)
        {
            WriteInput(By.Id("m_login_email"), sender.Username);
            WriteInput(By.Id("m_login_password"), sender.Password);

            if (TryFindElement(By.XPath("//button[contains(@data-sigil, 'm_login_button')]"), out var loginButton))
                loginButton.Click();

            WaitUntilBodyVisible();
        }

        public void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
            WaitUntilBodyVisible();
        }

        public void WaitUntil(Func<IWebDriver, IWebElement> waitCondition)
        {
            var wait = new WebDriverWait(driver, maxWaitingTime);
            wait.Until(d => waitCondition);
        }

        public void WaitUntilBodyVisible()
        {
            WaitUntil(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("body")));
        }

        public void WriteInput(By by, string inputValue)
        {
            if (TryFindElement(by, out var input))
                input.SendKeys(inputValue);
        }

        public bool TryFindElement(By by, out IWebElement element)
        {
            try
            {
                element = driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                element = default;
                return false;
            }
        }

        public void OpenStartPage()
        {
            NavigateTo(_facebookSettings.Mobile.Home);
        }
    }
}
