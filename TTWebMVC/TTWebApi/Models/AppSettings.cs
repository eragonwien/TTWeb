using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebApi.Models
{
   public class AppSettings
   {
      public string AuthSecret { get; set; }
      public int AuthTokenDurationDay { get; set; }
      public int RefreshAuthTokenLength { get; set; }
   }
}
