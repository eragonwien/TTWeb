using System.ComponentModel.DataAnnotations;

namespace TTWeb.BusinessLogic.Models.Account
{
    public class BoxLoginModel
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        public BoxLoginModel()
        {

        }

        public BoxLoginModel(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}