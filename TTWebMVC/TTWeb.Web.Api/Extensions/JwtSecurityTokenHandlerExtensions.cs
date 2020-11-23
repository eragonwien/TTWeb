using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.AppSettings;
using TTWeb.BusinessLogic.Models.AppSettings.Authentication;
using TTWeb.BusinessLogic.Models.AppSettings.Token;

namespace TTWeb.Web.Api.Extensions
{
    public static class JwtSecurityTokenHandlerExtensions
    {
        public static JwtSecurityToken CreateAccessToken(this JwtSecurityTokenHandler tokenHandler,
            AutheticationJsonWebTokenAppSettings settings,
            IEnumerable<Claim> claims)
        {
            return tokenHandler.CreateSecurityToken(settings.AccessToken.Key,
                settings.Issuer,
                DateTime.UtcNow.Add(settings.AccessToken.Duration),
                new ClaimsIdentity(claims));
        }

        public static JwtSecurityToken CreateRefreshToken(this JwtSecurityTokenHandler tokenHandler,
            AutheticationJsonWebTokenAppSettings settings)
        {
            return tokenHandler.CreateSecurityToken(settings.RefreshToken.Key,
                settings.Issuer,
                DateTime.UtcNow.Add(settings.RefreshToken.Duration));
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
