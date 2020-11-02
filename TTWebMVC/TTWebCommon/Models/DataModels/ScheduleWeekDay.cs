namespace TTWebCommon.Models.DataModels
{
    public class ScheduleWeekDay
    {
        public ScheduleWeekDay()
        {
        }

        public ScheduleWeekDay(int id, WeekDayEnum weekDay, string displayText)
        {
            Id = id;
            Name = weekDay;
            DisplayText = displayText;
        }

        public int Id { get; set; }
        public WeekDayEnum Name { get; set; }
        public string DisplayText { get; set; }
    }
}