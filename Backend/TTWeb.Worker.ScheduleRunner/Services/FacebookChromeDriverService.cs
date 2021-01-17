using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Services;
using TTWeb.Worker.ScheduleRunner.Extensions;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public class FacebookChromeDriverService : IFacebookChromeDriverService
    {
        private readonly IHostEnvironment _environment;
        private readonly AuthenticationProvidersFacebookAppSettings _facebookSettings;
        private readonly IHelperService _helper;
        private readonly StringBuilder logger = new();
        private ChromeDriver driver;

        private static readonly TimeSpan timeout = TimeSpan.FromSeconds(20);
        private const int maxRetryCount = 5;

        #region By-paths

        private readonly By _loginEmailInput = By.Id("m_login_email");
        private readonly By _loginPasswordInput = By.Id("m_login_password");
        private readonly By _loginButton = By.XPath("//button[contains(@data-sigil, 'm_login_button')]");
        private readonly By _twoFactorAuthenticationCodeInput = By.Id("approvals_code");
        private readonly By _checkpointSubmitActualButton = By.Id("checkpointSubmitButton-actual-button");
        private readonly By _checkpointSubmitButton = By.Id("checkpointSubmitButton");
        private readonly By _userStories = By.XPath("//article[contains(@class, 'async_like')][contains(@data-sigil, 'story-div')]");
        private readonly By _likeButtonOfStory = By.XPath(".//footer[contains(@class, '_22rc')]//*[contains(@data-sigil, 'ufi-inline-actions')]//*[contains(@class, '_52jj _15kl _3hwk')]/*[contains(@class, '_15ko _77li')][@role='button'][contains(@data-sigil, 'ufi-inline-like')][contains(@data-sigil, 'like-reaction-flyout')]");

        #endregion

        public FacebookChromeDriverService(IHostEnvironment environment,
            IOptions<AuthenticationAppSettings> authenticationAppSettingsOptions,
            IHelperService helper)
        {
            _environment = environment;
            _facebookSettings = authenticationAppSettingsOptions.Value.Providers.Facebook;
            _helper = helper;
        }

        public void AcceptCookieAgreement()
        {
            if (TryFindElement(By.Id("accept-cookie-banner-label"), out var element) && element.IsVisible())
            {
                element.Click();
                Log("Cookie Agreement accepted");
            }
        }

        public void Close()
        {
            driver?.Close();
            Log("Browser closed");
        }

        public void Launch()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-notifications");

            if (_environment.IsDevelopment())
                options.AddArgument("--start-maximized");

            driver = new ChromeDriver(options);
            Log("Browser launched");
        }

        public void LikeNewestStory()
        {
            var stories = GetUserStories();

            foreach (var story in stories)
            {
                var likeButton = GetLikeButton(story);
                if (likeButton == null)
                {
                    Log("Moving on to next story");
                    continue;
                }

                if (likeButton.HasAttributeOfValue("aria-pressed", true))
                {
                    Log("Like button was already pressed");
                    Log("Moving on to next story");
                    continue;
                }

                ClickAndWaitForPageLoad(likeButton);
                Log("Like button pressed");
                return;
            }

            Log("No like button was pressed");
        }

        private IWebElement GetLikeButton(IWebElement story)
        {
            if (!TryFindElement(_likeButtonOfStory, story, out var likeButton))
            {
                Log("Like button for story not found");
                likeButton = default;
            }

            return likeButton;
        }

        private ICollection<IWebElement> GetUserStories()
        {
            TryFindElements(_userStories, out var userStories);
            Log($"{userStories.Count} user stories found");
            return userStories;
        }

        public void Login(string username, string password)
        {
            WriteInput(_loginEmailInput, username, true);
            WriteInput(_loginPasswordInput, password, true);

            ClickAndWaitForPageLoad(_loginButton);
            Log("Login pressed");
        }

        public void OpenStartPage()
        {
            NavigateTo(_facebookSettings.Mobile.Home);
        }

        public void ByPassTwoFactorAuthentication(string seedCode)
        {
            if (string.IsNullOrWhiteSpace(seedCode)) return;

            var counter = 0;
            do
            {
                if (TryFindElement(_twoFactorAuthenticationCodeInput, out var codeInput))
                    Enter2ApprovalCode(codeInput);
                else if (TryFindElement(_checkpointSubmitButton, out var checkpointSubmitButton))
                    ClickAndWaitForPageLoad(checkpointSubmitButton);
                else if (TryFindElement(_checkpointSubmitActualButton, out var checkpointSubmitActualButton))
                    ClickAndWaitForPageLoad(checkpointSubmitActualButton);
                else
                    break;

                counter++;
                Sleep();
            } while (counter < maxRetryCount);
            Log($"Bypassing completed after {counter} tries");

            void Enter2ApprovalCode(IWebElement codeInput)
            {
                var approvalCode = _helper.GetOtpCode(seedCode);
                codeInput.SendKeys(approvalCode);
                ClickAndWaitForPageLoad(_checkpointSubmitActualButton);
            }
        }

        public void NavigateToUserProfile(string userCode)
        {
            NavigateTo($"{_facebookSettings.Mobile.Home}/{userCode}");
        }

        public void Sleep(TimeSpan? duration = null)
        {
            duration ??= TimeSpan.FromSeconds(3);
            Thread.Sleep(duration.Value);
        }

        private void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
            Log($"Navigate to {url}");
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
                ClickAndWaitForPageLoad(element);
            else
                WaitUntilBodyVisible();
        }

        private void ClickAndWaitForPageLoad(IWebElement element)
        {
            element.Click();
            WaitUntil(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
        }

        private void WriteInput(By by, string inputValue, bool required = false)
        {
            if (TryFindElement(by, out var input))
                input.SendKeys(inputValue);
            else if (required)
                throw new NotFoundException($"Required element {by} not found");
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

        private bool TryFindElement(By by, IWebElement source, out IWebElement element)
        {
            try
            {
                element = source.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                element = default;
                return false;
            }
        }

        private bool TryFindElements(By by, IWebElement source, out ICollection<IWebElement> elements)
        {
            try
            {
                elements = source.FindElements(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                elements = new List<IWebElement>();
                return false;
            }
        }

        private bool TryFindElements(By by, out ICollection<IWebElement> elements)
        {
            try
            {
                elements = driver.FindElements(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                elements = new List<IWebElement>();
                return false;
            }
        }

        private bool ElementExists(By by)
        {
            return TryFindElement(by, out var element);
        }

        private void Log(string message)
        {
            logger.AppendLine(message);
        }

        public string BuildLogMessage()
        {
            return logger.ToString();
        }

        public void Start(FacebookUserModel sender)
        {
            OpenStartPage();
            AcceptCookieAgreement();
            Login(sender.Username, sender.Password);
            Sleep();

            ByPassTwoFactorAuthentication(sender.SeedCode);
            Sleep();
        }
    }
}
