using System;

namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class SchedulingPlanningAppSettings
    {
        public int CountPerRequest { get; set; }
        public TimeSpan LockDuration { get; set; }
    }
}