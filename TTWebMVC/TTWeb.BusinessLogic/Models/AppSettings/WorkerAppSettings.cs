namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class WorkerAppSettings
    {
        public const string Section = "Worker";

        public int ClientId { get; set; }

        public string ClientSecret { get; set; }

        public int? TokenLifeTimeMultiplier { get; set; }
    }
}