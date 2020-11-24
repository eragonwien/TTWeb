using Microsoft.IdentityModel.Tokens;

namespace TTWeb.BusinessLogic.Models.AppSettings.Authentication
{
    public class AuthenticationJsonWebTokenAppSettings
    {
        public string Issuer { get; set; }
        public AuthenticationJsonWebTokenTokenAppSettings AccessToken { get; set; }
        public AuthenticationJsonWebTokenTokenAppSettings RefreshToken { get; set; }

        public TokenValidationParameters TokenValidationDefaultParameters => new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidIssuer = Issuer
        };
    }
}