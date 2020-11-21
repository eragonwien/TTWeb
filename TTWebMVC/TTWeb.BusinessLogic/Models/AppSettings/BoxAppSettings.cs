using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.AppSettings.Token;

namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class BoxAppSettings
    {
        public const string Section = "Box";

        public int ClientId { get; set; }
        public string ClientSecret { get; set; }
        public JsonWebTokenAppSettings JsonWebToken { get; set; }
    }
}