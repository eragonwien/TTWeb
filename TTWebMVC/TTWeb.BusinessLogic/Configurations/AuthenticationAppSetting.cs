﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TTWeb.BusinessLogic.Configurations
{
    public class AuthenticationAppSetting
    {
        public const string SectionName = "Authentication";
        public string ExternalCookieScheme { get; set; }
        public TimeSpan CookieExpirationTimeSpan { get; set; }
        public AuthenticationFacebookAppSetting Facebook { get; set; }
    }
}
