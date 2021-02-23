using System;
using System.Threading;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;

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
                _browser.Launch(cancellationToken);
                switch (job.Action)
                {
                    case Data.Models.ScheduleAction.Like:
                        Like(job);
                        break;
                    case Data.Models.ScheduleAction.Comment:
                        Comment(job);
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
            result.Message = _browser.BuildLogMessage();

            return result;
        }

        private void Like(ScheduleJobModel job)
        {
            _browser.Start(job.Sender);

            _browser.NavigateToUserProfile(job.Receiver.UserCode);
            _browser.Sleep();

            _browser.LikeNewestStory();
        }

        private void Comment(ScheduleJobModel job)
        {
            _browser.Start(job.Sender);

            _browser.NavigateToUserProfile(job.Receiver.UserCode);
            _browser.Sleep();

            _browser.Comment(job);
        }

        private void Post()
        {
            throw new NotImplementedException();
        }
    }
}