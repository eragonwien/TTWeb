﻿using TTWeb.BusinessLogic.Models.AppSettings.Token;

namespace TTWeb.BusinessLogic.Models.AppSettings.Authentication
{
    public class AuthenticationAppSettings
    {
        public const string Section = "Authentication";

        public JsonWebTokenAppSettings JsonWebToken { get; set; }
        public AuthenticationProvidersAppSettings Providers { get; set; }
    }
}