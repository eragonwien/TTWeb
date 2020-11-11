using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TTWeb.Data.Models
{
    public class FacebookUser : IUserOwnedEntity, IHasIdEntity
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        
        public string Password { get; set; }

        public string HomeAddress { get; set; }

        public string ProfileAddress { get; set; }

        public bool Enabled { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public virtual ICollection<Schedule> SendSchedule { get; set; }
        public virtual ICollection<ScheduleReceiverMapping> ScheduleReceiverMappings { get; set; }
        public virtual LoginUser Owner { get; set; }
    }
}