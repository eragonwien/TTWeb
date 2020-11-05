namespace TTWeb.BusinessLogic.Models.Entities
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
