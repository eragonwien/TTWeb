using System;
using System.Collections.Generic;
using System.Linq;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models
{
    public class ScheduleJobModel : BaseUserOwnedModel
    {
        private readonly DateTime _currentDateTime = DateTime.UtcNow;
        private readonly Random _random = new Random();

        public int ScheduleId { get; set; }

        public ScheduleAction Action { get; set; }

        public ScheduleIntervalType IntervalType { get; set; }

        public FacebookUserModel Sender { get; set; }

        public FacebookUserModel Receiver { get; set; }

        public IEnumerable<DayOfWeek> Weekdays { get; set; }

        public TimeSpan? From { get; set; }

        public TimeSpan? To { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? StartTime { get; set; }

        public ScheduleJobModel CalculateStartTime()
        {
            return IntervalType switch
            {
                ScheduleIntervalType.Once => CalculateStartTimeOnce(),
                ScheduleIntervalType.Daily => CalculateStartTimeDaily(),
                ScheduleIntervalType.Weekly => CalculateStartTimeWeekly(),
                ScheduleIntervalType.Monthly => CalculateStartTimeMonthly(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public ScheduleJobModel WithReceiver(int receiverId)
        {
            Receiver ??= new FacebookUserModel();

            Receiver.Id = receiverId;

            return this;
        }

        public ScheduleJobModel WithTimeFrame(ScheduleTimeFrame timeFrame)
        {
            if (timeFrame == null) throw new ArgumentNullException(nameof(timeFrame));

            From = timeFrame.From;
            To = timeFrame.To;

            return this;
        }

        private ScheduleJobModel CalculateStartTimeOnce()
        {
            if (!StartDate.HasValue) throw new ArgumentException(nameof(StartDate));
            if (!StartTime.HasValue) throw new ArgumentException(nameof(StartTime));

            return this;
        }

        private ScheduleJobModel CalculateStartTimeDaily()
        {
            if (!From.HasValue) throw new ArgumentException(nameof(From));
            if (!To.HasValue) throw new ArgumentException(nameof(To));

            var startTime = _currentDateTime.Date.Add(From.Value);
            var endTime = _currentDateTime.Date.Add(To.Value);

            // End date goes to the next day if it is smaller then start date
            if (endTime < startTime)
                endTime = endTime.AddDays(1);

            StartTime = RandomizeStartTime(startTime, endTime);
            StartDate = StartTime.Value.Date;
            return this;
        }

        private ScheduleJobModel CalculateStartTimeWeekly()
        {
            return Weekdays.Contains(_currentDateTime.DayOfWeek) ? CalculateStartTimeDaily() : this;
        }

        private ScheduleJobModel CalculateStartTimeMonthly()
        {
            // TODO: Requires day of month property
            throw new NotImplementedException();
        }

        private DateTime RandomizeStartTime(DateTime startTime, DateTime endTime)
        {
            if (endTime < startTime)
                throw new ArgumentException($"Start date of {startTime} should be smaller then end date of {endTime}");
            var diff = endTime - startTime;
            var randomDiff = new TimeSpan(0, _random.Next(0, (int)diff.TotalMinutes), 0);
            return startTime + randomDiff;
        }
    }
}