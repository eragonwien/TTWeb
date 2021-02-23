namespace TTWeb.BusinessLogic.Models
{
    public class AuthenticationProvidersFacebookAppSettings
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public AuthenticationProvidersFacebookMobileAppSettings Mobile { get; set; }
    }
}