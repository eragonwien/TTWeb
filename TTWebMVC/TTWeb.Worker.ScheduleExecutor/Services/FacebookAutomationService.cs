using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public class FacebookAutomationService : IFacebookAutomationService
    {
        private ScheduleJobModel job;
        private ChromeDriver driver;
        private readonly IHostEnvironment environment;
        private readonly AuthenticationProvidersFacebookAppSettings facebookSettings;

        public FacebookAutomationService(IHostEnvironment environment,
            IOptions<AuthenticationAppSettings> authenticationAppSetingsOptions)
        {
            this.environment = environment;
            facebookSettings = authenticationAppSetingsOptions.Value.Providers.Facebook;
        }

        public async Task ProcessAsync(ScheduleJobModel workingJob)
        {
            if (workingJob is null) throw new ArgumentNullException(nameof(workingJob));
            job = workingJob;

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

                default:
                    break;
            }
        }

        private async Task LikeAsync()
        {
            LauchBrowser();
            driver.Navigate().GoToUrl(facebookSettings.Mobile.Home);
        }

        private Task CommentAsync()
        {
            throw new NotImplementedException();
        }

        private Task PostAsync()
        {
            throw new NotImplementedException();
        }

        private void LauchBrowser()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-notifications");

            if (environment.IsDevelopment())
            {
                options.AddArgument("--start-maximized");
            }

            driver = new ChromeDriver(Environment.CurrentDirectory, options);
        }
    }
}