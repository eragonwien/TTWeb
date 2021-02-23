using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TTWeb.BusinessLogic.Models
{
    public class LoginTokenValidationResult
    {
        public bool Succeed { get; set; }
        public SecurityToken Token { get; set; }
        public ClaimsPrincipal TokenUser { get; set; }
    }
}