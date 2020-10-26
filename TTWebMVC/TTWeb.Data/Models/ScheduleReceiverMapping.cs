namespace TTWeb.Data.Models
{
    public class ScheduleReceiverMapping
    {
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public int ReceiverId { get; set; }
        public FacebookUser Receiver { get; set; }
    }
}
