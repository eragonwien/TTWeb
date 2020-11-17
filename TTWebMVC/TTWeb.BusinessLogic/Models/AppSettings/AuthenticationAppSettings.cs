namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class AuthenticationAppSettings
    {
        public const string Section = "Authentication";

        public AuthenticationJsonWebTokenAppSettings JsonWebToken { get; set; }
        public AuthenticationProvidersAppSettings Providers { get; set; }
    }
}