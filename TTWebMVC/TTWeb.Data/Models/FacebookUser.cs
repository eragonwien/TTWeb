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

        [Required]
        public int OwnerId { get; set; }

        public ICollection<Schedule> SendSchedule { get; set; }
        public ICollection<ScheduleReceiverMapping> ScheduleReceiverMappings { get; set; }
        public LoginUser Owner { get; set; }
    }
}