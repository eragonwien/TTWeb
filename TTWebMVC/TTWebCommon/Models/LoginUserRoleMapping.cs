using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTWebCommon.Models
{
   public class LoginUserRoleMapping
   {
      [Column("loginuser_id")]
      public int LoginUserId { get; set; }
      public LoginUser LoginUser { get; set; }
      [Column("loginuserrole_id")]
      public int LoginUserRoleId { get; set; }
      public LoginUserRole LoginUserRole { get; set; }
   }
}
