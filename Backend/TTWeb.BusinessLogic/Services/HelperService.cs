using OtpNet;
using System;
using System.Globalization;
using System.IO;
using TTWeb.BusinessLogic.Extensions;

namespace TTWeb.BusinessLogic.Services
{
    public class HelperService : IHelperService
    {
        public string GetOtpCode(string seed)
        {
            if (string.IsNullOrWhiteSpace(seed)) return string.Empty;

            seed = seed.RemoveWhiteSpace().ToUpper(CultureInfo.InvariantCulture);

            var generator = new Totp(Base32Encoding.ToBytes(seed));
            return generator.ComputeTotp(DateTime.UtcNow);
        }

        public string GetRandomString(int length)
        {
            return Path.GetRandomFileName().Replace(".", "");
        }
    }
}