using System.Collections.Generic;
using OpenQA.Selenium;

namespace TTWeb.Worker.ScheduleRunner.Models
{
    public class FacebookStoryModel
    {
        public IWebElement WebElement { get; set; }
        public List<string> HashTags { get; set; } = new List<string>();

        public FacebookStoryModel(IWebElement webElement)
        {
            WebElement = webElement;
        }
    }
}
