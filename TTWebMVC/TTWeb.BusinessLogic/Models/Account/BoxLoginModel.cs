using System.ComponentModel.DataAnnotations;

namespace TTWeb.BusinessLogic.Models.Account
{
    public class BoxLoginModel
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
}