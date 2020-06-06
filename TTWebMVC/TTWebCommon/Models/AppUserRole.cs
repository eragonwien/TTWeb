using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTWebCommon.Models
{
   [Table("AppUserRole")]
   public class AppUserRole
   {
      [Column("appuser_id")]
      public int AppUserId { get; set; }
      public AppUser AppUser { get; set; }
      [Column("role_id")]
      public int RoleId { get; set; }
      public Role Role { get; set; }
   }
}
