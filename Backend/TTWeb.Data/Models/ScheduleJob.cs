using System;
using System.Collections.Generic;

namespace TTWeb.Data.Models
{
    public class ScheduleJob : IHasIdEntity
    {
        public int ScheduleId { get; set; }

        public ScheduleAction Action { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public ProcessingStatus Status { get; set; }

        public DateTime? LockAt { get; set; }

        public DateTime? LockedUntil { get; set; }

        public Schedule Schedule { get; set; }

        public FacebookUser Sender { get; set; }

        public FacebookUser Receiver { get; set; }

        public int RetryCount { get; set; }

        public int MaxRetryCount { get; set; }

        public ICollection<ScheduleJobResult> Results { get; set; }
        public int Id { get; set; }

        public ScheduleJob WithStatus(ProcessingStatus status)
        {
            Status = status;
            return this;
        }

        public ScheduleJob Lock(DateTime lockDate, TimeSpan lockDuration)
        {
            LockAt = lockDate;
            LockedUntil = LockAt.Value.Add(lockDuration);
            Status = ProcessingStatus.InProgress;
            return this;
        }
    }
}