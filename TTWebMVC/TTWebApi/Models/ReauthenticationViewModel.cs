using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebApi.Models
{
   public class ReauthenticationViewModel
   {
      [Required]
      public string AccessToken { get; set; }
      [Required]
      public string RefreshToken { get; set; }
   }
}
