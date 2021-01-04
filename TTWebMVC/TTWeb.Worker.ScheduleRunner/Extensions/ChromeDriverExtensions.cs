using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium;

namespace TTWeb.Worker.ScheduleRunner.Extensions
{
    public static class ChromeDriverExtensions
    {
        private static readonly TimeSpan maxWaitingTime = TimeSpan.FromSeconds(20);

        public static void NavigateTo(this ChromeDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.WaitUntilBodyVisible();
        }

        public static bool TryFindElement(this ChromeDriver driver, By by, out IWebElement element)
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

        #region Facebook

        public static void AcceptCookieAgreement(this ChromeDriver driver)
        {
            if (driver.TryFindElement(By.Id("accept-cookie-banner-label"), out var element) && element.IsVisible())
                element.Click();
        }

        public static void GetPostings(this ChromeDriver driver)
        {

        }

        public static void Like(this ChromeDriver driver)
        {

        }

        #endregion

        #region Form

        public static void WriteInput(this ChromeDriver driver, By by, string value)
        {
            if (driver.TryFindElement(by, out var input))
                input.SendKeys(value);
        }

        #endregion

        #region Wait

        public static void WaitUntil(this ChromeDriver driver, Func<IWebDriver, IWebElement> waitCondition)
        {
            var wait = new WebDriverWait(driver, maxWaitingTime);
            wait.Until(d => waitCondition);
        }

        public static void WaitUntilBodyVisible(this ChromeDriver driver)
        {
            driver.WaitUntil(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("body")));
        }

        #endregion
    }
}
