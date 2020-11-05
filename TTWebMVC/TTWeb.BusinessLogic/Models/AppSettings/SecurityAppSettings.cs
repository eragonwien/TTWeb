namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class SecurityAppSettings
    {
        public const string Section = "Security";

        public SecurityEncryptionAppSettings Encryption { get; set; }
        public SecurityCorsAppSettings Cors { get; set; }
    }

    public class SecurityEncryptionAppSettings
    {
        public const string Section = "Encryption";

        public string Key { get; set; }
        public string Iv { get; set; }
    }

    public class SecurityCorsAppSettings
    {
        public const string Section = "Cors";

        public string[] Origins { get; set; } = new string[0];
    }
}