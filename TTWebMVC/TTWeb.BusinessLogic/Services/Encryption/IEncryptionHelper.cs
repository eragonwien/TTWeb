namespace TTWeb.BusinessLogic.Services.Encryption
{
    public interface IEncryptionHelper
    {
        string Encrypt(string plainText, string passPhrase);
        string Decrypt(string encryptedText, string passPhrase);
    }
}
