namespace TTWeb.BusinessLogic.Services
{
    public interface IEncryptionHelper
    {
        string Encrypt(string plainText);

        string Decrypt(string encryptedText);
    }
}