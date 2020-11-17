namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class SecurityCorsAppSettings
    {
        public const string Section = "Cors";

        public string[] Origins { get; set; } = new string[0];
    }
}