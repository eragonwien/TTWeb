namespace TTWeb.BusinessLogic.Models
{
    public class AuthenticationAppSettings
    {
        public const string Section = "Authentication";

        public AuthenticationJsonWebTokenAppSettings JsonWebToken { get; set; }
        public AuthenticationProvidersAppSettings Providers { get; set; }
    }
}