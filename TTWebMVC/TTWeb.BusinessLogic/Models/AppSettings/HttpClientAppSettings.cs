namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class HttpClientAppSettings
    {
        public const string Section = "HttpClient";
        public WebApiAppSettings WebApi { get; set; }
    }
}