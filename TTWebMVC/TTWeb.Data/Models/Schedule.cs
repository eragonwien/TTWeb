using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TTWeb.Data.Models
{
    public class Schedule : IUserOwnedEntity, IHasIdEntity, IHasWorkerEntity
    {
        public int Id { get; set; }

        [Required]
        public ScheduleAction Action { get; set; }

        [Required]
        public ScheduleIntervalType IntervalType { get; set; }

        [Required]
        public ProcessingStatus PlanningStatus { get; set; }

        [Required]
        public int? SenderId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public int? WorkerId { get; set; }

        public DateTime? LockedUntil { get; set; }

        public FacebookUser Sender { get; set; }
        public LoginUser Owner { get; set; }
        public LoginUser Worker { get; set; }
        public ICollection<ScheduleReceiverMapping> ScheduleReceiverMappings { get; set; }
        public ICollection<ScheduleWeekdayMapping> ScheduleWeekdayMappings { get; set; }
        public ICollection<ScheduleTimeFrame> TimeFrames { get; set; }
        public ICollection<ScheduleJob> ScheduleJobs { get; set; }
        
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