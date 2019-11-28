using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVC.Models
{
   public abstract class BaseUser
   {
      public long Id { get; set; } = 0;
      public string Email { get; set; }
      public string Firstname { get; set; }
      public string Lastname { get; set; }
      public string FacebookId { get; set; }

      public bool IsValid()
      {
         return Id > 0 && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(FacebookId);
      }
   }

   public class AppUser : BaseUser
   {
      public string AccessToken { get; set; }
      public DateTime? AccessTokenExpirationDate { get; set; }


   }

   public class Partner : BaseUser
   {
      public List<UserGroup> UserGroups { get; set; }
   }
}
