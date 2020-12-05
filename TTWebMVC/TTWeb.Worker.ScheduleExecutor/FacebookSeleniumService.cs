using System;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.Worker.ScheduleExecutor
{
    public class FacebookSeleniumService : IFacebookSeleniumService
    {
        public async Task ProcessAsync(ScheduleJobModel workingJob)
        {
            if (workingJob is null) throw new ArgumentNullException(nameof(workingJob));

            switch (workingJob.Action)
            {
                case Data.Models.ScheduleAction.Like:
                    await LikeAsync(workingJob);
                    break;
                case Data.Models.ScheduleAction.Comment:
                    await CommentAsync(workingJob);
                    break;
                case Data.Models.ScheduleAction.Post:
                    await PostAsync(workingJob);
                    break;
                default:
                    break;
            }
        }

        private Task LikeAsync(ScheduleJobModel workingJob)
        {
            if (workingJob is null) throw new ArgumentNullException(nameof(workingJob));
            throw new NotImplementedException();
        }

        private Task CommentAsync(ScheduleJobModel workingJob)
        {
            throw new NotImplementedException();
        }

        private Task PostAsync(ScheduleJobModel workingJob)
        {
            throw new NotImplementedException();
        }
    }
}
