using System;
using Newtonsoft.Json;

namespace TTWeb.BusinessLogic.Models.Account
{
    public class TokenModel
    {
        public string Token { get; set; }

        public DateTime ExpirationDateUtc { get; set; }

        [JsonIgnore] public bool IsEmpty => string.IsNullOrWhiteSpace(Token);

        [JsonIgnore] public bool Expired => ExpirationDateUtc < DateTime.UtcNow;
    }
}