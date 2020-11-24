using Microsoft.IdentityModel.Tokens;
using System;

namespace TTWeb.BusinessLogic.Models.AppSettings.Token
{
    public class AutheticationJsonWebTokenAppSettings
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

        public AutheticationJsonWebTokenAppSettings Merge(WorkerAppSettings workerAppSettings)
        {
            if (workerAppSettings is null) throw new ArgumentNullException(nameof(workerAppSettings));
            if (!workerAppSettings.TokenLifeTimeMultiplier.HasValue) return this;

            AccessToken.Duration = TimeSpan.FromTicks(AccessToken.Duration.Ticks * workerAppSettings.TokenLifeTimeMultiplier.Value);
            RefreshToken.Duration = TimeSpan.FromTicks(RefreshToken.Duration.Ticks * workerAppSettings.TokenLifeTimeMultiplier.Value);

            return this;
        }
    }
}