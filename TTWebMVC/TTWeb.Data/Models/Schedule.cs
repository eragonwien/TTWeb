using System;
using System.Collections.Generic;

namespace TTWeb.Data.Models
{
    public class Schedule : IUserOwnedEntity, IHasIdEntity
    {
        public int Id { get; set; }
        public ScheduleAction Action { get; set; }
        public ScheduleIntervalType IntervalType { get; set; }
        public int? SenderId { get; set; }
        public FacebookUser Sender { get; set; }
        public ICollection<ScheduleReceiverMapping> ScheduleReceiverMappings { get; set; }
        public ICollection<ScheduleWeekdayMapping> ScheduleWeekdayMappings { get; set; }
        public ICollection<ScheduleTimeFrame> TimeFrames { get; set; }
        public ICollection<ScheduleJob> ScheduleJobs { get; set; }
        public int OwnerId { get; set; }
        public LoginUser Owner { get; set; }
        public ProcessingStatus PlanningStatus { get; set; }
        public DateTime? LockedUntil { get; set; }
        public int? WorkerId { get; set; }
        public LoginUser Worker { get; set; }

        public Schedule LockUntil(DateTime? lockTime)
        {
            LockedUntil = lockTime;
            return this;
        }

        public Schedule SetStatus(ProcessingStatus status)
        {
            PlanningStatus = status;
            return this;
        }
    }
}