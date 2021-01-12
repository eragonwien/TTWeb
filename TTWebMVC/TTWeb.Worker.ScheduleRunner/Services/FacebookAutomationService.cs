using System;
using System.Threading;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;

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
            _browser.Login(job.Sender.Username, job.Sender.Password);
            _browser.Sleep();

            _browser.ByPassTwoFactorAuthentication(job.Sender.SeedCode);
            _browser.Sleep();
           
            _browser.NavigateToUserProfile(job.Receiver.UserCode);
            _browser.Sleep();

            _browser.Like(1, 5);
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