using OpenQA.Selenium;
using System;
using System.Globalization;

namespace TTWeb.Worker.ScheduleRunner.Extensions
{
    public static class WebElementExtensions
    {
        public static bool IsVisible(this IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }

        public static bool HasAttributeOfValue(this IWebElement element, string attributeName, object value)
        {
            try
            {
                var attribute = element.GetAttribute(attributeName);
                return attribute?.ToLower(CultureInfo.InvariantCulture) == value?.ToString().ToLower(CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}