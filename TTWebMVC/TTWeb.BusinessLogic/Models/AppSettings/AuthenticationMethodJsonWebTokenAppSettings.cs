using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class AuthenticationMethodJsonWebTokenAppSettings
    {
        public string AccessTokenSecret { get; set; }
        public string RefreshTokenSecret { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenDurationMinutes { get; set; }
        public int RefreshTokenDurationDays { get; set; }

        public TokenValidationParameters AccessTokenParameters => new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AccessTokenSecret)),
            ClockSkew = TimeSpan.Zero
        };

        public TokenValidationParameters RefreshTokenParameters => new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AccessTokenSecret)),
            ClockSkew = TimeSpan.Zero
        };
    }
}