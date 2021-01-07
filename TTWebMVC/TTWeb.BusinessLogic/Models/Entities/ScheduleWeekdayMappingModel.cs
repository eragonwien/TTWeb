using System;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleWeekdayMappingModel : IHasScheduleIdModel
    {
        public ScheduleWeekdayMappingModel(int scheduleId, DayOfWeek weekday)
        {
            ScheduleId = scheduleId;
            Weekday = weekday;
        }

        public DayOfWeek Weekday { get; set; }
        public int ScheduleId { get; set; }
    }
}