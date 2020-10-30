using System;
using System.Security.Cryptography;

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
