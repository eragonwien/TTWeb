using TTWeb.BusinessLogic.Models.AppSettings;

namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class AuthenticationProvidersFacebookAppSettings
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public AuthenticationProvidersFacebookMobileAppSettings Mobile { get; set; }
    }
}