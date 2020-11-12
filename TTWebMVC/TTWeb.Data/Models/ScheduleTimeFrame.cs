using System;

namespace TTWeb.Data.Models
{
    public class ScheduleTimeFrame
    {
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}