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
        }

        public static void WaitUntil(this ChromeDriver driver, Func<IWebDriver, IWebElement> waitCondition)
        {
            var wait = new WebDriverWait(driver, maxWaitingTime);
            wait.Until(d => waitCondition);
        }
    }
}
