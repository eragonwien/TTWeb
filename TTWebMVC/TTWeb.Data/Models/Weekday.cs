using System;
using System.Collections.Generic;

namespace TTWeb.Data.Models
{
    public class Weekday
    {
        public int Id { get; set; }
        public DayOfWeek Value { get; set; }
        public ICollection<ScheduleWeekdayMapping> ScheduleWeekdayMappings { get; set; }
    }
}
