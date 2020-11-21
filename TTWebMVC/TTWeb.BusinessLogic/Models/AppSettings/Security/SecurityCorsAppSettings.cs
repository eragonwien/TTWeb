namespace TTWeb.BusinessLogic.Models.AppSettings.Security
{
    public class SecurityCorsAppSettings
    {
        public const string Section = "Cors";

        public string[] Origins { get; set; } = new string[0];
    }
}