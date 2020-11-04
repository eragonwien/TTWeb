using System.ComponentModel.DataAnnotations;

namespace TTWeb.BusinessLogic.Models.Account
{
    public class ExternalLoginModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}