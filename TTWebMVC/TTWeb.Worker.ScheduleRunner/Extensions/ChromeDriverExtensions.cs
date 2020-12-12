using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace TTWeb.Worker.ScheduleRunner.Extensions
{
    public static class ChromeDriverExtensions
    {
        private static TimeSpan maxWaitingTime = TimeSpan.FromSeconds(20);

        public static void NavigateTo(this ChromeDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void WaitUntil(this ChromeDriver driver, Func<bool> waitCondition)
        {
            var wait = new WebDriverWait(driver, maxWaitingTime);
            wait.Until(d => waitCondition);
        }

        public static void WaitFor(this ChromeDriver driver, TimeSpan waitingTime)
        {
            if (waitingTime > maxWaitingTime)
                waitingTime = maxWaitingTime;

            var now = DateTime.UtcNow;
            driver.WaitUntil(() => DateTime.UtcNow > now.Add(waitingTime));
        }
    }
}
