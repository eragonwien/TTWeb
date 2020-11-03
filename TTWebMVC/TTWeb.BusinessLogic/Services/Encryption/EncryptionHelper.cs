using System;
using TTWeb.BusinessLogic.Models.AppSettings;

namespace TTWeb.BusinessLogic.Services.Encryption
{
    public class EncryptionHelper : IEncryptionHelper
    {
        private readonly EncryptionAppSettings _encryptionAppSettings;

        public EncryptionHelper(EncryptionAppSettings encryptionAppSettings)
        {
            _encryptionAppSettings = encryptionAppSettings;
        }

        public string Encrypt(string plainText)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string encryptedText)
        {
            throw new NotImplementedException();
        }
    }
}