using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebMVCV2.Models
{
   public class FacebookCredentialCreateViewModel
   {
      [Required]
      public string Username { get; set; }
      [Required]
      public string Password { get; set; }

      public FacebookCredentialCreateViewModel()
      {

      }

      public FacebookCredentialCreateViewModel(FacebookCredential credential)
      {
         Username = credential.Username;
         Password = credential.Password;
      }
   }
   public class FacebookCredentialUpdateViewModel : FacebookCredentialCreateViewModel
   {
      [Required]
      public int Id { get; set; }

      public FacebookCredentialUpdateViewModel()
      {

      }

      public FacebookCredentialUpdateViewModel(FacebookCredential credential) : base(credential)
      {
         Id = credential.Id;
      }
   }
}
