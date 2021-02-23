using System;

namespace TTWeb.BusinessLogic.Models
{
    public class AuthenticationJsonWebTokenTokenAppSettings
    {
        public string Key { get; set; }
        public TimeSpan Duration { get; set; }
    }
}