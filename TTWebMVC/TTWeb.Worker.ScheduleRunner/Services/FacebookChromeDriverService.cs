using System;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TTWeb.BusinessLogic.Extensions;
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

        private static readonly TimeSpan timeout = TimeSpan.FromSeconds(20);
        private const int maxRetryCount = 5;

        #region By-paths

        private readonly By _loginEmailInput = By.Id("m_login_email");
        private readonly By _loginPasswordInput = By.Id("m_login_password");
        private readonly By _loginButton = By.XPath("//button[contains(@data-sigil, 'm_login_button')]");
        private readonly By _twoFactorAuthenticationCodeInput = By.Id("approvals_code");
        private readonly By _twoFactorAuthenticationSendButton = By.Id("checkpointSubmitButton-actual-button");

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

            ClickAndWaitForPageLoad(_loginButton);
        }

        public void OpenStartPage()
        {
            NavigateTo(_facebookSettings.Mobile.Home);
        }

        public void ByPassTwoFactorAuthentication(string seedCode)
        {
            if (string.IsNullOrWhiteSpace(seedCode)) return;

            for (var i = 0; i < maxRetryCount; i++)
            {
                if (!TryFindElement(_twoFactorAuthenticationCodeInput, out var codeInput))
                    return;

                var approvalCode = _otp.GetCode(seedCode.RemoveWhiteSpace().ToUpper());
                codeInput.SendKeys(approvalCode);
                ClickAndWaitForPageLoad(_twoFactorAuthenticationSendButton);
            }
        }

        public void NavigateToUserProfile(string userCode)
        {
            NavigateTo($"{_facebookSettings}/{userCode}");
        }

        public void Sleep(TimeSpan? duration = null)
        {
            duration ??= TimeSpan.FromSeconds(1);
            Thread.Sleep(duration.Value);
        }

        private void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
            WaitUntilBodyVisible();
        }

        private void WaitUntil<TResult>(Func<IWebDriver, TResult> waitCondition)
        {
            var wait = new WebDriverWait(driver, timeout);
            wait.Until(d => waitCondition);
        }

        private void WaitUntilBodyVisible()
        {
            WaitUntil(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("body")));
        }

        private void ClickAndWaitForPageLoad(By by)
        {
            if (TryFindElement(by, out var element))
            {
                element.Click();
                WaitUntil(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
            }
            else
            {
                WaitUntilBodyVisible();
            }
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
