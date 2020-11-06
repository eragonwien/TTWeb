using System;
using Microsoft.IdentityModel.Tokens;

namespace TTWeb.BusinessLogic.Extensions
{
    public static class TokenValidationParametersExtensions
    {
        public static TokenValidationParameters ValidateLifeTime(this TokenValidationParameters parameters,
            bool validate = true)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            parameters.ValidateLifetime = validate;
            return parameters;
        }
    }
}
