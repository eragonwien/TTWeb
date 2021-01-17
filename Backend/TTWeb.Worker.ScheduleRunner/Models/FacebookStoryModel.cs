using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace TTWeb.Worker.ScheduleRunner.Models
{
    public class FacebookStoryModel
    {
        public IWebElement WebElement { get; set; }
        public List<string> Contents { get; set; } = new List<string>();

        public FacebookStoryModel(IWebElement webElement)
        {
            WebElement = webElement;
            LoadContents();
        }

        private FacebookStoryModel LoadContents()
        {
            if (WebElement == null) return this;



            return this;
        }
    }
}
