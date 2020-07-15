using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVCV2.Models
{
   public class FacebookFriendViewModel
   {
      public int? Id { get; set; } = 0;
      [Required]
      public string Name { get; set; }
      [Required]
      public string ProfileLink { get; set; }
   }
}
