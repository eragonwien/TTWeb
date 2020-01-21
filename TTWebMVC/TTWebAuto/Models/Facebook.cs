using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebAuto.Models
{
   public class FacebookFriends
   {
      public string Link { get; set; }
      public string Name { get; set; }
   }

   public class FacebookCredentials
   {
      public string Email { get; set; }
      public string Password { get; set; }

      public FacebookCredentials(string email, string password)
      {
         Email = email;
         Password = password;
      }
   }
}
