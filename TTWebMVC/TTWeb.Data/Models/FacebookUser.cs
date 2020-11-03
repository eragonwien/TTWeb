using System.Collections.Generic;

namespace TTWeb.Data.Models
{
    public class FacebookUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Schedule> SendSchedule { get; set; }
        public ICollection<ScheduleReceiverMapping> ScheduleReceiverMappings { get; set; }
        public int OwnerId { get; set; }
        public LoginUser Owner { get; set; }
    }
}