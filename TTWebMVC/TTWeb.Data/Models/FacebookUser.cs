using System.Collections.Generic;

namespace TTWeb.Data.Models
{
    public class FacebookUser : IUserOwnedEntity, IHasIdEntity
    {
        public int Id { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }

        public string HomeAddress { get; set; }

        public string ProfileAddress { get; set; }

        public bool Enabled { get; set; }

        public int OwnerId { get; set; }

        public ICollection<Schedule> SenderSchedules { get; set; }
        //public ICollection<ScheduleJob> SenderScheduleJobs { get; set; }
        //public ICollection<ScheduleJob> ReceiverScheduleJobs { get; set; }
        public ICollection<ScheduleReceiverMapping> ScheduleReceiverMappings { get; set; }
        public LoginUser Owner { get; set; }
    }
}