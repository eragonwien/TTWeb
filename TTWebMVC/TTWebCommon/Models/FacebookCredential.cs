using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TTWebCommon.Models
{
   public class FacebookCredential
   {
      public int Id { get; set; } = 0;
      [Required]
      public string Username { get; set; }
      public string Password { get; set; }
   }
}
