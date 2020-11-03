using Microsoft.Extensions.Options;
using NETCore.Encrypt;
using TTWeb.BusinessLogic.Models.AppSettings;

namespace TTWeb.BusinessLogic.Services.Encryption
{
    public class EncryptionHelper : IEncryptionHelper
    {
        private readonly EncryptionAppSettings _encryptionAppSettings;

        public EncryptionHelper(IOptions<EncryptionAppSettings> encryptionAppSettings)
        {
            _encryptionAppSettings = encryptionAppSettings.Value;
        }

        public string Encrypt(string plainText)
        {
            return EncryptProvider.AESEncrypt(plainText, _encryptionAppSettings.Key, _encryptionAppSettings.Iv);
        }

        public string Decrypt(string encryptedText)
        {
            return EncryptProvider.AESDecrypt(encryptedText, _encryptionAppSettings.Key, _encryptionAppSettings.Iv);
        }
    }
}