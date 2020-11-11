using System;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleWeekdayMappingModel : IHasScheduleIdModel
    {
        public int ScheduleId { get; set; }
        public DayOfWeek Weekday { get; set; }

        public ScheduleWeekdayMappingModel(int scheduleId, DayOfWeek weekday)
        {
            ScheduleId = scheduleId;
            Weekday = weekday;
        }
    }
}