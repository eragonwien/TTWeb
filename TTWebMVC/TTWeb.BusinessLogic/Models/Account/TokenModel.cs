using System;

namespace TTWeb.BusinessLogic.Models.Account
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
    }
}