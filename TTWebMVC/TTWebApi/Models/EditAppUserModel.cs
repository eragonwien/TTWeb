using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebApi.Models
{
   public class EditAppUserModel
   {
      [Required]
      public int Id { get; set; }
      public string Firstname { get; set; }
      public string Lastname { get; set; }
      [Required]
      [DataType(DataType.EmailAddress)]
      public string Email { get; set; }

      public AppUser ToAppUser()
      {
         return new AppUser
         {
            Id = Id,
            Firstname = Firstname,
            Lastname = Lastname,
            Email = Email
         };
      }
   }
}
