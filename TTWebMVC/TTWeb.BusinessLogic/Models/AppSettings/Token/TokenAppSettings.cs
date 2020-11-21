using System;

namespace TTWeb.BusinessLogic.Models.AppSettings.Token
{
    public class TokenAppSettings
    {
        public string Key { get; set; }
        public TimeSpan Duration { get; set; }
    }
}