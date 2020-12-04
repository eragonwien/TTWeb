﻿using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;

namespace TTWeb.Worker.ScheduleExecutor
{
    public interface IFacebookSeleniumService
    {
        Task ProcessAsync(ScheduleJobModel workingJob);
    }
}
