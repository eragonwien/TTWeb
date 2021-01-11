using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.Worker.ScheduleRunner.Extensions;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public class FacebookAutomationService : IFacebookAutomationService
    {
        private readonly IFacebookChromeDriverService _browser;

        public FacebookAutomationService(IFacebookChromeDriverService browser)
        {
            _browser = browser;
        }

        public async Task<ProcessingResult<ScheduleJobModel>> ProcessAsync(ScheduleJobModel job,
            CancellationToken cancellationToken)
        {
            if (job is null) throw new ArgumentNullException(nameof(job));
            var result = new ProcessingResult<ScheduleJobModel>(result: job);

            try
            {
                _browser.Launch();
                switch (job.Action)
                {
                    case Data.Models.ScheduleAction.Like:
                        Like(job);
                        break;
                    case Data.Models.ScheduleAction.Comment:
                        Comment();
                        break;
                    case Data.Models.ScheduleAction.Post:
                        Post();
                        break;
                }

                result.Succeed = true;
            }
            catch (Exception ex)
            {
                result.Succeed = false;
                result.Reason = $"{ex}";
            }
            finally
            {
                _browser.Close();
            }

            return new ProcessingResult<ScheduleJobModel>(succeed: true, result: job);
        }

        private void Like(ScheduleJobModel job)
        {
            _browser.OpenStartPage();
            _browser.AcceptCookieAgreement();
            _browser.Login(job.Sender);
            _browser.ByPassTwoFactorAuthentication(job.Sender);
            _browser.NavigateTo(job.Receiver.UserCode);
            _browser.GetPostings();
            _browser.Like();
        }

        private void Comment()
        {
            throw new NotImplementedException();
        }

        private void Post()
        {
            throw new NotImplementedException();
        }
    }
}