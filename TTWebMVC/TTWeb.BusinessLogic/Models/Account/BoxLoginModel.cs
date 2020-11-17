using System.ComponentModel.DataAnnotations;

namespace TTWeb.BusinessLogic.Models.Account
{
    public class BoxLoginModel
    {
        [Required]
        public string BoxId { get; set; }

        [Required]
        public string BoxSecret { get; set; }
    }
}