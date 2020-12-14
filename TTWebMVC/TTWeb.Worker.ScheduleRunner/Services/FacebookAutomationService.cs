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
using System.Security.Cryptography;

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
            driver.AcceptCookieAgreement();
            Login();
            driver.NavigateTo("new address");
            driver.GetPostings();
            driver.Like();
        }

        private void Login()
        {
            driver.WriteInput(By.Id("email"), "email");
            driver.WriteInput(By.Id("password"), "password");

            if (driver.TryFindElement(By.Id("loginButton"), out var loginButton))
                loginButton.Click();

            driver.WaitUntilBodyVisible();
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