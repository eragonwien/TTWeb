using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVC.Models.Facebook
{
   public class FacebookTokenInfo
   {
      public string AccessToken { get; set; }
      public DateTimeOffset ExpirationDate { get; set; }

      public static FacebookTokenInfo FromResponse(FacebookAccessTokenResponse response)
      {
         return new FacebookTokenInfo
         {
            AccessToken = response.Access_token,
            ExpirationDate = DateTimeOffset.Now.AddSeconds(response.Expires_in)
         };
      }
   }

   public class FacebookPageInfo
   {
      public long Id { get; set; }
      public string Name { get; set; }
      public string AccessToken { get; set; }
      public string Category { get; set; }

      public static List<FacebookPageInfo> FromResponse(FacebookAccountsResponse response)
      {
         return response.Data
            .Select(d => new FacebookPageInfo
            {
               Id = d.Id,
               Name = d.Name,
               AccessToken = d.Access_token,
               Category = d.Category
            })
            .ToList();
      }
   }
}
