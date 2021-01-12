using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.Helper.Otp;
using TTWeb.Worker.ScheduleRunner.Extensions;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public class FacebookChromeDriverService : IFacebookChromeDriverService
    {
        private readonly IHostEnvironment _environment;
        private readonly AuthenticationProvidersFacebookAppSettings _facebookSettings;
        private readonly IOtpHelperService _otp;
        private ChromeDriver driver;

        private static readonly TimeSpan maxWaitingTime = TimeSpan.FromSeconds(20);
        private const int maxRetryCount = 5;

        #region By-paths

        private readonly By _loginEmailInput = By.Id("m_login_email");
        private readonly By _loginPasswordInput = By.Id("m_login_password");
        private readonly By _loginButton = By.XPath("//button[contains(@data-sigil, 'm_login_button')]");
        private readonly By _twoFactorAuthenticationCodeInput = By.Id("approvals_code");
        private readonly By _twoFactorAuthenticationButton = By.Id("checkpointSubmitButton-actual-button");

        #endregion

        public FacebookChromeDriverService(IHostEnvironment environment,
            IOptions<AuthenticationAppSettings> authenticationAppSettingsOptions,
            IOtpHelperService otp)
        {
            _environment = environment;
            _facebookSettings = authenticationAppSettingsOptions.Value.Providers.Facebook;
            _otp = otp;
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

        public void Launch()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-notifications");

            if (_environment.IsDevelopment())
                options.AddArgument("--start-maximized");

            driver = new ChromeDriver(options);
        }

        public void Like(int likeCount, int maxPostCount)
        {
            throw new NotImplementedException();
        }

        public void Login(string username, string password)
        {
            WriteInput(_loginEmailInput, username);
            WriteInput(_loginPasswordInput, password);

            if (TryFindElement(_loginButton, out var loginButton))
                loginButton.Click();

            WaitUntilBodyVisible();
        }

        public void OpenStartPage()
        {
            NavigateTo(_facebookSettings.Mobile.Home);
        }

        public void ByPassTwoFactorAuthentication(string seedCode)
        {
            for (var i = 0; i < maxRetryCount; i++)
            {
                WaitUntilBodyVisible();

                if (!TryFindElement(_twoFactorAuthenticationCodeInput, out var codeInput))
                    return;

                if (!TryFindElement(_twoFactorAuthenticationButton, out var sendButton))
                    return;

                var approvalCode = _otp.GetCode(seedCode);
                codeInput.SendKeys(approvalCode);
                sendButton.Click();
            }
        }

        public void NavigateToUserProfile(string userCode)
        {
            NavigateTo($"{_facebookSettings}/{userCode}");
        }

        private void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
            WaitUntilBodyVisible();
        }

        private void WaitUntil(Func<IWebDriver, IWebElement> waitCondition)
        {
            var wait = new WebDriverWait(driver, maxWaitingTime);
            wait.Until(d => waitCondition);
        }

        private void WaitUntilBodyVisible()
        {
            WaitUntil(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("body")));
        }

        private void WriteInput(By by, string inputValue)
        {
            if (TryFindElement(by, out var input))
                input.SendKeys(inputValue);
        }

        private bool TryFindElement(By by, out IWebElement element)
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
    }
}
