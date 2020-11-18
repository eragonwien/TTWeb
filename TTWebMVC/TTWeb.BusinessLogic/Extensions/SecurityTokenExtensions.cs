using System;
using Microsoft.IdentityModel.Tokens;

namespace TTWeb.BusinessLogic.Extensions
{
    public static class SecurityTokenExtensions
    {
        // TODO: merges redundant method
        public static bool IsAlmostExpired(this SecurityToken token, TimeSpan tolerantTimeSpan)
        {
            var timeLeft = DateTime.UtcNow - token.ValidTo;
            return timeLeft <= tolerantTimeSpan;
        }
    }
}