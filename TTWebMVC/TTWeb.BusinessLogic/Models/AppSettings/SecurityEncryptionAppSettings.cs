namespace TTWeb.BusinessLogic.Models.AppSettings
{
    public class SecurityEncryptionAppSettings
    {
        public const string Section = "Encryption";

        public string Key { get; set; }
        public string Iv { get; set; }
    }
}