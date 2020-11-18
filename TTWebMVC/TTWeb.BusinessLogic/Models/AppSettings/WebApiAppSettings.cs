namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class WebApiAppSettings
    {
        public readonly string ClientName = nameof(HttpClientAppSettings.WebApi);

        public string BaseAddress { get; set; }
        public string UserAgent { get; set; }
        public WebApiRoutesAppSettings Routes { get; set; }

        public string GetRoute(params string[] routeParts)
        {
            return string.Join("/", BaseAddress, routeParts);
        }
    }
}