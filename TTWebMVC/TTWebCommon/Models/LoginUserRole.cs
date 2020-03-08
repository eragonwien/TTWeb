using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebCommon.Models
{
   public class LoginUserRole
   {
      public int Id { get; set; }
      public LoginUserRoleEnum Name { get; set; }
   }

   public enum LoginUserRoleEnum
   {
      ADMIN,
      REGULAR
   }
}
