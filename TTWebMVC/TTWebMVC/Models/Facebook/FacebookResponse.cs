using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVC.Models.Facebook
{
   public class FacebookAccessTokenResponse
   {
      public string Access_token { get; set; }
      public double Expires_in { get; set; }
   }

   public class FacebookAccountsResponse
   {
      public List<FacebookAccountsResponseData> Data { get; set; }
   }

   public class FacebookAccountsResponseData
   {
      public long Id { get; set; }
      public string Access_token { get; set; }
      public string Name { get; set; }
      public string Category { get; set; }
   }
}
