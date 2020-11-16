using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleJobModel : BaseUserOwnedModel
    {
        [Required]
        public int ScheduleId { get; set; }

        [Required]
        public ScheduleAction Action { get; set; }

        [Required]
        public ScheduleIntervalType IntervalType { get; set; }

        [Required]
        public ScheduleFacebookUserModel Sender { get; set; }

        [Required]
        public ScheduleFacebookUserModel Receiver { get; set; }

        [Required]
        public IEnumerable<DayOfWeek> Weekdays { get; set; }

        [Required]
        public TimeSpan? From { get; set; }

        [Required]
        public TimeSpan? To { get; set; }

        public int? WorkerId { get; set; }

        public DateTime? StartDate { get; set; }

        private readonly DateTime _currentDateTime = DateTime.UtcNow;
        private readonly Random _random = new Random();

        public ScheduleJobModel CalculateStartTime()
        {
            switch (IntervalType)
            {
                case ScheduleIntervalType.Once:
                    return CalculateStartTimeOnce();
                case ScheduleIntervalType.Daily:
                    return CalculateStartTimeDaily();
                case ScheduleIntervalType.Weekly:
                    return CalculateStartTimeWeekly();
                case ScheduleIntervalType.Monthly:
                    return CalculateStartTimeMonthly();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ScheduleJobModel WithReceiver(int receiverId)
        {
            if (Receiver == null) Receiver = new ScheduleFacebookUserModel();

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
            // TODO: Requires exact date property
            throw new NotImplementedException();
        }

        private ScheduleJobModel CalculateStartTimeDaily()
        {
            if (!From.HasValue) throw new ArgumentException(nameof(From));
            if (!To.HasValue) throw new ArgumentException(nameof(To));

            var startDate = _currentDateTime.Date.Add(From.Value);
            var endDate = _currentDateTime.Date.Add(To.Value);

            // End date goes to the next day if it is smaller then start date
            if (endDate < startDate)
                endDate = endDate.AddDays(1);

            StartDate = RandomizeStartDate(startDate, endDate);
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

        private DateTime RandomizeStartDate(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate) throw new ArgumentException($"Start date of {startDate} should be smaller then end date of {endDate}");
            var diff = endDate - startDate;
            var randomDiff = new TimeSpan(0, _random.Next(0, (int)diff.TotalMinutes), 0);
            return startDate + randomDiff;
        }
    }
}
