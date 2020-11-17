namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class WebApiAppSettings
    {
        public const string Section = "WebApi";

        public string BaseUri { get; set; }
        public WebApiAppSettings Routes { get; set; }
    }
}