using Microsoft.IdentityModel.Tokens;
using System;

namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class WebApiAppSettings
    {
        public const string Section = "WebApi";

        public string BaseUri { get; set; }
        public WebApiRoutesAppSettings Routes { get; set; }

        public string GetRoute(string routePostFix)
        {
            return string.Join("/", BaseUri, routePostFix);
        }
    }
}