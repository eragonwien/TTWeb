using System;
using System.Collections.Generic;
using System.Text;

namespace TTWeb.Data.Models
{
    public class FacebookUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Schedule> SendSchedule { get; set; }
        public ICollection<ScheduleReceiverMapping> ScheduleReceiverMappings { get; set; }
    }
}
