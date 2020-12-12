using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Internal;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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
            using var driver = LauchBrowser();
            driver.NavigateTo(facebookSettings.Mobile.Home);

            driver.WaitFor(TimeSpan.FromSeconds(10));

        }

        private Task CommentAsync()
        {
            throw new NotImplementedException();
        }

        private Task PostAsync()
        {
            throw new NotImplementedException();
        }

        private ChromeDriver LauchBrowser()
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