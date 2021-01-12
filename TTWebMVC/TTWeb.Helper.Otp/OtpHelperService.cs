﻿using System;
using OtpNet;

namespace TTWeb.Helper.Otp
{
    public class OtpHelperService : IOtpHelperService
    {
        public string GetCode(string seed)
        {
            var seedBytes = Base32Encoding.ToBytes(seed);
            var totp = new Totp(seedBytes);
            return totp.ComputeTotp(DateTime.UtcNow);
        }
    }
}
