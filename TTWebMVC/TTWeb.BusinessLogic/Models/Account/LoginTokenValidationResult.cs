using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TTWeb.BusinessLogic.Models.Account
{
    public class LoginTokenValidationResult
    {
        public bool Succeed { get; set; }
        public SecurityToken Token { get; set; }
        public ClaimsPrincipal TokenUser { get; set; }
    }
}