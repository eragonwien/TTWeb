using OpenQA.Selenium;
using System;
using System.Collections.Generic;

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
