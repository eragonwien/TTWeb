﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebApi.Models
{
   public class ChangePasswordModel
   {
      public string OldPassword { get; set; }
      public string NewPassword { get; set; }
      public string ConfirmNewPassword { get; set; }
   }
}
