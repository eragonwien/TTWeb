using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVC.Models
{
   public class UserGroup
   {
      public long Id { get; set; }
      public string Title { get; set; }
      public AppUser AppUser { get; set; }
      public List<Partner> Partners { get; set; }

      public UserGroup(long id, string title, AppUser appUser)
      {
         Id = id;
         Title = title;
         AppUser = appUser;
      }
   }
}
