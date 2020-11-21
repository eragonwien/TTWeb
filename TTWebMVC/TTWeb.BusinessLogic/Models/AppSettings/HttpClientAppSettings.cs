using TTWeb.BusinessLogic.Models.AppSettings.WebApi;

namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class HttpClientAppSettings
    {
        public const string Section = "HttpClient";
        public const string AcceptHeaderDefault = "application/json";
        public WebApiAppSettings WebApi { get; set; }
    }
}