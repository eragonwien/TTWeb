namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class SecurityAppSettings
    {
        public const string Section = "Security";

        public SecurityEncryptionAppSettings Encryption { get; set; }
        public SecurityCorsAppSettings Cors { get; set; }
    }
}