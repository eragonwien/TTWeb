using System;
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

        public AuthenticationJsonWebTokenAppSettings ExtendTokenDuration(int? tokenLifeTimeMultiplier)
        {
            if (!tokenLifeTimeMultiplier.HasValue) return this;

            AccessToken.Duration = TimeSpan.FromTicks(AccessToken.Duration.Ticks * tokenLifeTimeMultiplier.Value);
            RefreshToken.Duration = TimeSpan.FromTicks(RefreshToken.Duration.Ticks * tokenLifeTimeMultiplier.Value);

            return this;
        }
    }
}