using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.AppSettings;

namespace TTWeb.Web.Api.Extensions
{
    public static class JwtSecurityTokenHandlerExtensions
    {
        public static JwtSecurityToken CreateAccessToken(this JwtSecurityTokenHandler tokenHandler,
            AuthenticationAppSettings settings,
            IEnumerable<Claim> claims)
        {
            return tokenHandler.CreateSecurityToken(settings.Methods.JsonWebToken.AccessTokenSecret,
                settings.Methods.JsonWebToken.Issuer,
                DateTime.UtcNow.AddMinutes(settings.Methods.JsonWebToken.AccessTokenDurationMinutes),
                new ClaimsIdentity(claims));
        }

        public static JwtSecurityToken CreateRefreshToken(this JwtSecurityTokenHandler tokenHandler,
            AuthenticationAppSettings settings)
        {
            return tokenHandler.CreateSecurityToken(settings.Methods.JsonWebToken.RefreshTokenSecret,
                settings.Methods.JsonWebToken.Issuer,
                DateTime.UtcNow.AddDays(settings.Methods.JsonWebToken.RefreshTokenDurationDays));
        }

        public static LoginTokenValidationResult ValidateToken(this JwtSecurityTokenHandler tokenHandler,
            TokenModel accessToken,
            TokenValidationParameters tokenValidationParameters)
        {
            var result = new LoginTokenValidationResult();
            try
            {
                result.TokenUser = tokenHandler.ValidateToken(accessToken.Token,
                     tokenValidationParameters,
                     out var token);
                result.Token = token;
                result.Succeed = true;
            }
            catch
            {
                result.Succeed = false;
            }

            return result;
        }

        private static JwtSecurityToken CreateSecurityToken(this JwtSecurityTokenHandler tokenHandler,
            string secretKey,
            string issuer,
            DateTime expirationDateUtc,
            ClaimsIdentity subject = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            return tokenHandler.CreateJwtSecurityToken(issuer,
                subject: subject,
                expires: expirationDateUtc,
                signingCredentials: signingCredentials);
        }
    }
}
