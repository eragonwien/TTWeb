namespace TTWeb.BusinessLogic.Services.Encryption
{
    public interface IEncryptionHelper
    {
        string Encrypt(string plainText);

        string Decrypt(string encryptedText);
    }
}