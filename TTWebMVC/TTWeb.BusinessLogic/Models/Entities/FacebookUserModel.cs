using System.ComponentModel.DataAnnotations;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class FacebookUserModel
    {
        public int? Id { get; set; }

        [Required]
        public int? OwnerId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public FacebookUserModel ClearPassword()
        {
            Password = string.Empty;
            return this;
        }
    }
}
