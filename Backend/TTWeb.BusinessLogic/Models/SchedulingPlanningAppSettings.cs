using System;

namespace TTWeb.BusinessLogic.Models
{
    public class SchedulingPlanningAppSettings
    {
        public int CountPerRequest { get; set; }
        public TimeSpan LockDuration { get; set; }

        public TimeSpan TriggerInterval { get; set; }
    }
}