using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TTWebCommon.Models
{
   public class AppUser
   {
      public int Id { get; set; }
      public string Email { get; set; }
      public string Title { get; set; }
      public string Firstname { get; set; }
      public string Lastname { get; set; }
      public bool Disabled { get; set; }
      public bool Active { get; set; }
      public string FacebookUser { get; set; }
      public List<UserRoleEnum> Roles { get; set; } = new List<UserRoleEnum>();
   }
}
