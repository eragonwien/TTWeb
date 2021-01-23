using System;
using Microsoft.Extensions.Options;
using NETCore.Encrypt;
using TTWeb.BusinessLogic.Models.AppSettings.Security;

namespace TTWeb.BusinessLogic.Services
{
    public class EncryptionHelper : IEncryptionHelper
    {
        private readonly SecurityEncryptionAppSettings _encryptionAppSettings;

        public EncryptionHelper(IOptions<SecurityAppSettings> securityAppSettings)
        {
            _encryptionAppSettings = securityAppSettings.Value.Encryption;
        }

        public string Encrypt(string plainText)
        {
            if (plainText == null) return null;

            return EncryptProvider.AESEncrypt(plainText, _encryptionAppSettings.Key, _encryptionAppSettings.Iv);
        }

        public string Decrypt(string encryptedText)
        {
            if (encryptedText == null) return null;

            return EncryptProvider.AESDecrypt(encryptedText, _encryptionAppSettings.Key, _encryptionAppSettings.Iv);
        }
    }
}