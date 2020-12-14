using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Worker.ScheduleRunner.Extensions;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public class FacebookAutomationService : IFacebookAutomationService
    {
        private ScheduleJobModel job;
        private readonly IHostEnvironment environment;
        private readonly AuthenticationProvidersFacebookAppSettings facebookSettings;
        private ChromeDriver driver;

        public FacebookAutomationService(IHostEnvironment environment,
            IOptions<AuthenticationAppSettings> authenticationAppSettingsOptions)
        {
            this.environment = environment;
            facebookSettings = authenticationAppSettingsOptions.Value.Providers.Facebook;
        }

        public async Task ProcessAsync(ScheduleJobModel workingJob)
        {
            job = workingJob ?? throw new ArgumentNullException(nameof(workingJob));
            driver = LaunchBrowser();
            switch (workingJob.Action)
            {
                case Data.Models.ScheduleAction.Like:
                    await LikeAsync();
                    break;
                case Data.Models.ScheduleAction.Comment:
                    await CommentAsync();
                    break;
                case Data.Models.ScheduleAction.Post:
                    await PostAsync();
                    break;
            }
            driver.Close();
        }

        private async Task LikeAsync()
        {
            driver.NavigateTo(facebookSettings.Mobile.Home);
            driver.WaitUntil(ElementIsVisible(By.TagName("body")));

            if (TryFindElement(By.Id("accept-cookie-banner-label"), out var element) && element.IsVisible())
            {
                element.Click();
                driver.WaitUntil(ElementIsVisible(By.TagName("body")));
            }
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

        private bool IsVisible(IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }

        private Task CommentAsync()
        {
            throw new NotImplementedException();
        }

        private Task PostAsync()
        {
            throw new NotImplementedException();
        }

        private ChromeDriver LaunchBrowser()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-notifications");

            if (environment.IsDevelopment())
            {
                options.AddArgument("--start-maximized");
            }

            return new ChromeDriver(options);
        }
    }
}