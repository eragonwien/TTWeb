using System;

namespace TTWeb.BusinessLogic.Models.AppSettings.Scheduling
{
    public class SchedulingJobAppSettings
    {
        public int CountPerRequest { get; set; }

        public TimeSpan LockDuration { get; set; }

        public TimeSpan TriggerInterval { get; set; }
    }
}