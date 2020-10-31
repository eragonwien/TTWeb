using System;

namespace TTWeb.BusinessLogic.Models.Account
{
    public class LoginTokenModel
    {
        public string Token { get; set; }
        public DateTime TokenExpirationDateUtc { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDateUtc { get; set; }
    }
}
