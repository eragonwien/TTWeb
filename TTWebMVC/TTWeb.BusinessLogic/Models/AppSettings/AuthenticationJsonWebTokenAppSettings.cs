using Microsoft.IdentityModel.Tokens;

namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class AuthenticationJsonWebTokenAppSettings
    {
        public string Issuer { get; set; }
        public AuthenticationJsonWebTokenAccessTokenAppSettings AccessToken { get; set; }
        public AuthenticationJsonWebTokenRefreshTokenAppSettings RefreshToken { get; set; }

        public TokenValidationParameters TokenValidationDefaultParameters => new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidIssuer = Issuer
        };
    }
}