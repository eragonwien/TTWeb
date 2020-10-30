﻿using System.ComponentModel.DataAnnotations;
using TTWebCommon.Models.DataModels;

namespace TTWebMVCV2.Models.ViewModels
{
   public class FacebookCredentialViewModel
   {
      public int? Id { get; set; }
      [Required]
      public string Username { get; set; }
      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      public FacebookCredentialViewModel()
      {

      }

      public FacebookCredentialViewModel(FacebookCredential credential)
      {
         Username = credential.Username;
         Password = credential.Password;
      }
   }
}
