namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class AuthenticationMethodJsonWebTokenAppSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenDurationMinutes { get; set; }
        public int RefreshTokenDurationDays { get; set; }
    }
}