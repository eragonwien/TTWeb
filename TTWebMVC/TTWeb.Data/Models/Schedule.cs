using System.Collections.Generic;

namespace TTWeb.Data.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public ScheduleAction Action { get; set; }
        public ScheduleIntervalType IntervalType { get; set; }
        public int SenderId { get; set; }
        public FacebookUser Sender { get; set; }
        public ICollection<ScheduleReceiverMapping> ScheduleReceiverMappings { get; set; }
        public ICollection<ScheduleWeekdayMapping> ScheduleWeekdayMappings { get; set; }
        public ICollection<TimeFrame> TimeFrames { get; set; }
        public ICollection<ScheduleJob> ScheduleJobs { get; set; }
    }
}
