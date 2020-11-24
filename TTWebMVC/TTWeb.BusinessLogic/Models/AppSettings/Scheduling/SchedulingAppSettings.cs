using System;

namespace TTWeb.BusinessLogic.Models.AppSettings.Scheduling
{
    public class SchedulingAppSettings
    {
        public const string Section = "Scheduling";

        public SchedulingPlanningAppSettings Planning { get; set; }

        public SchedulingJobAppSettings Job { get; set; }

    }
}
