namespace TTWeb.Data.Models
{
    public class ScheduleJobResult : IHasIdEntity
    {
        public int Id { get; set; }
        public int ScheduleJobId { get; set; }
        public ProcessingStatus Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }

        public ScheduleJob ScheduleJob { get; set; }

        // TODO: Adds more details, Adds mapping profile, Updates model
    }
}