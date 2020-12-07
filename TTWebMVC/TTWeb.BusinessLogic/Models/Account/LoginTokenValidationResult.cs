using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace TTWeb.BusinessLogic.Models.Account
{
    public class LoginTokenValidationResult
    {
        public bool Succeed { get; set; }
        public SecurityToken Token { get; set; }
        public ClaimsPrincipal TokenUser { get; set; }
    }
}