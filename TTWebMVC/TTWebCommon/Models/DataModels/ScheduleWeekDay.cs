using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebCommon.Models.DataModels
{
    public class ScheduleWeekDay
    {
        public int Id { get; set; }
        public WeekDayEnum Name { get; set; }
        public string DisplayText { get; set; }

        public ScheduleWeekDay()
        {
        }

        public ScheduleWeekDay(int id, WeekDayEnum weekDay, string displayText)
        {
            Id = id;
            Name = weekDay;
            DisplayText = displayText;
        }
    }
}
