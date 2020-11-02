namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class AuthenticationAppSettings
    {
        public const string Section = "Authentication";

        public AuthenticationMethodsAppSettings Methods { get; set; }
        public AuthenticationProvidersAppSettings Providers { get; set; }
    }
}