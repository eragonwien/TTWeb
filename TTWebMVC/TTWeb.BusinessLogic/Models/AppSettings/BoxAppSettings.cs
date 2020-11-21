using TTWeb.BusinessLogic.Models.AppSettings.Authentication;

namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class BoxAppSettings
    {
        public const string Section = "Box";

        public int ClientId { get; set; }
        public string ClientSecret { get; set; }
        public AuthenticationJsonWebTokenAppSettings JsonWebToken { get; set; }
    }
}