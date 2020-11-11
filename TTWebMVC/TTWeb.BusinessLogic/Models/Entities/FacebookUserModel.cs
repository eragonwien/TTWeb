using System.ComponentModel.DataAnnotations;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class FacebookUserModel : BaseUserOwnedModel
    {
        [Required]
        public string Username { get; set; }

        public string Password { get; set; }

        public string HomeAddress { get; set; }

        public string ProfileAddress { get; set; }

        public bool Enabled { get; set; }

        public FacebookUserModel ClearPassword()
        {
            Password = string.Empty;
            return this;
        }
    }
}
