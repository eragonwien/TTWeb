using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

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

        public static TokenValidationParameters WithKey(this TokenValidationParameters parameters,
            string key)
        {
            if (key is null) throw new ArgumentNullException(nameof(key));

            parameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return parameters;
        }
    }
}