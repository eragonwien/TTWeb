using System;

namespace TTWeb.BusinessLogic.Models.Account
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime ExpirationDateUtc { get; set; }

        public bool IsEmpty => string.IsNullOrWhiteSpace(Token);
        public bool Expired => ExpirationDateUtc < DateTime.UtcNow;

        public bool IsAlmostExpired(TimeSpan maxDuration)
        {
            var tolerantTimeSpan = new TimeSpan(maxDuration.Ticks / 2);
            var timeLeft = DateTime.UtcNow - ExpirationDateUtc;
            return timeLeft <= tolerantTimeSpan;
        }
    }
}