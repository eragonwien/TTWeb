namespace TTWeb.BusinessLogic.Models.Entities.Encryption
{
    public class EncryptionKeyModel
    {
        public string Key { get; set; }
        public string Iv { get; set; }

        public EncryptionKeyModel(string key, string iv)
        {
            Key = key;
            Iv = iv;
        }
    }
}
