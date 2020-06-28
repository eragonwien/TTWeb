using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace TTWebCommon.Services
{
   public static class AuthenticationHelper
   {
      public static string GenerateRandomToken(int length)
      {
         var randomNumber = new byte[length];
         using var rng = RandomNumberGenerator.Create();
         rng.GetBytes(randomNumber);
         return Convert.ToBase64String(randomNumber);
      }
   }
}
