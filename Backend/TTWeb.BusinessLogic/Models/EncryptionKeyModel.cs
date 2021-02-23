namespace TTWeb.BusinessLogic.Models
{
    public class EncryptionKeyModel
    {
        public EncryptionKeyModel(string key, string iv)
        {
            Key = key;
            Iv = iv;
        }

        public string Key { get; set; }
        public string Iv { get; set; }
    }
}