using System;

namespace TTWeb.Data.Models
{
    public class ScheduleWeekdayMapping
    {
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public int WeekdayId { get; set; }
        public DayOfWeek Weekday { get; set; }
    }
}
