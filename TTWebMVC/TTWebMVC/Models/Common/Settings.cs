using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVC.Models.Common
{
   public class Settings
   {
      public const string NlogConfigFileName = "nlog.config";
      public const string DefaultConnectionString = "Default";

      public const string CultureEnglish = "en";
      public const string CultureGerman = "de";
   }

   public class AuthenticationSettings
   {
      public const string SchemeApplication = "Application";
      public const string SchemeExternal = "External";

      public const string TokenAccessToken = "access_token";
      public const string TokenExpiredAt = "expires_at";

      public const string ClaimTypeAccessToken = "AccessToken";
      public const string ClaimTypeExpiredAt = "AccessTokenExpiredAt";
   }
}
