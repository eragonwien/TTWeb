namespace TTWeb.Data.Models
{
    public class ScheduleJobResult : IHasIdEntity
    {
        public int ScheduleJobId { get; set; }
        public ScheduleJob ScheduleJob { get; set; }
        public int Id { get; set; }

        // TODO: Adds more details, Adds mapping profile, Updates model
    }
}