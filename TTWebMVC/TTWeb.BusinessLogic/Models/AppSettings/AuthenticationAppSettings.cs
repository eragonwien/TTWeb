namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class AuthenticationAppSettings
    {
        public AuthenticationMethodsAppSettings Methods { get; set; }
        public AuthenticationProvidersAppSettings Providers { get; set; }
    }
}
