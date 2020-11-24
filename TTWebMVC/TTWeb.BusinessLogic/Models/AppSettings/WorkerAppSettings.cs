using TTWeb.BusinessLogic.Models.AppSettings.Authentication;

namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class WorkerAppSettings
    {
        public const string Section = "Worker";

        public int ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}