using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TTWebCommon.Models.DataModels;

namespace TTWebMVCV2.Models.ViewModels
{
   public class ProfileUserViewModel
   {
      [Required]
      public int Id { get; set; }
      [Required]
      [DataType(DataType.EmailAddress)]
      public string Email { get; set; }
      public string Title { get; set; }
      public string Firstname { get; set; }
      public string Lastname { get; set; }
      public List<FacebookCredential> FacebookCredentials { get; set; } = new List<FacebookCredential>();
      public List<FacebookFriend> FacebookFriends { get; set; } = new List<FacebookFriend>();

      public ProfileUserViewModel()
      {

      }

      public ProfileUserViewModel(AppUser appUser)
      {
         if (appUser != null)
         {
            Id = appUser.Id;
            Email = appUser.Email;
            Title = appUser.Title;
            Firstname = appUser.Firstname;
            Lastname = appUser.Lastname;
         }
      }

      public AppUser ToAppUser()
      {
         var appUser = new AppUser
         {
            Id = Id,
            Email = Email,
            Firstname = Firstname,
            Lastname = Lastname
         };

         return appUser;
      }
   }
}
