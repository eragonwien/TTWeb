using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTWebCommon.Models
{
   [Table("Role")]
   public class Role
   {
      public int Id { get; set; }
      public LoginUserRoleEnum Name { get; set; }
      [JsonIgnore]
      public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
   }

   public enum LoginUserRoleEnum
   {
      ACTIVATE_USER,
      VIEW_USERS
   }
}
