using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Worker.ScheduleRunner.Extensions;

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

        public async Task<bool> ProcessAsync(ScheduleJobModel workingJob, CancellationToken cancellationToken)
        {
            job = workingJob ?? throw new ArgumentNullException(nameof(workingJob));
            driver = LaunchBrowser();
            switch (workingJob.Action)
            {
                case Data.Models.ScheduleAction.Like:
                    Like();
                    break;
                case Data.Models.ScheduleAction.Comment:
                    Comment();
                    break;
                case Data.Models.ScheduleAction.Post:
                    Post();
                    break;
            }
            driver.Close();
            return true;
        }

        private void Like()
        {
            driver.NavigateTo(facebookSettings.Mobile.Home);
            driver.AcceptCookieAgreement();
            Login(job.Sender);
            driver.NavigateTo("new address");
            driver.GetPostings();
            driver.Like();
        }

        private void Login(ScheduleFacebookUserModel user)
        {
            driver.WriteInput(By.Id("email"), user.Username);
            driver.WriteInput(By.Id("password"), user.Password);

            if (driver.TryFindElement(By.Id("loginButton"), out var loginButton))
                loginButton.Click();

            driver.WaitUntilBodyVisible();
        }

        private void Comment()
        {
            throw new NotImplementedException();
        }

        private void Post()
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