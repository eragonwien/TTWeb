using System;

namespace TTWeb.Data.Models
{
    public class ScheduleJobResult : IHasIdEntity
    {
        public int Id { get; set; }
        public int ScheduleJobId { get; set; }
        public ProcessingStatus Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreateDate { get; set; }

        // TODO: Adds OwnerId property

        public ScheduleJob ScheduleJob { get; set; }
    }
}