using OpenQA.Selenium;

namespace TTWeb.Worker.ScheduleRunner.Extensions
{
    public static class WebElementExtensions
    {
        public static bool IsVisible(this IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }
    }
}
