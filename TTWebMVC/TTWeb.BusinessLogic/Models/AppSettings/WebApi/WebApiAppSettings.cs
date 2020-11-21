namespace TTWeb.BusinessLogic.Models.AppSettings.WebApi
{
    public class WebApiAppSettings
    {
        public string BaseAddress { get; set; }
        public string UserAgent { get; set; }
        public WebApiRoutesAppSettings Routes { get; set; }

        public string GetRoute(params string[] routeParts)
        {
            return string.Join("/", BaseAddress, routeParts);
        }
    }
}