using System;
using System.Collections.Generic;

namespace TTWeb.Data.Models
{
    public class ScheduleJob : IHasIdEntity
    {
        public int Id { get; set; }

        public int ScheduleId { get; set; }

        public ScheduleAction Action { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ProcessingStatus Status { get; set; }

        public Schedule Schedule { get; set; }

        public FacebookUser Sender { get; set; }

        public FacebookUser Receiver { get; set; }

        public ICollection<ScheduleJobResult> Results { get; set; }

        public ScheduleJob WithStatus(ProcessingStatus status)
        {
            Status = status;
            return this;
        }

        public ScheduleJob Lock(DateTime lockDate)
        {
            // TODO: locks job by date
            Status = ProcessingStatus.InProgress;
            return this;
        }
    }
}