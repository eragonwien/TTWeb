using System;

namespace TTWeb.BusinessLogic.Models.AppSettings.Authentication
{
    public class AuthenticationJsonWebTokenAccessTokenAppSettings
    {
        public string Key { get; set; }
        public TimeSpan Duration { get; set; }
    }
}