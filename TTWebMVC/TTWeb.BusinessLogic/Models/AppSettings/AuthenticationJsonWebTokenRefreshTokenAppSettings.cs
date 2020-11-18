using System;

namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class AuthenticationJsonWebTokenRefreshTokenAppSettings
    {
        public string Key { get; set; }
        public TimeSpan Duration { get; set; }
    }
}