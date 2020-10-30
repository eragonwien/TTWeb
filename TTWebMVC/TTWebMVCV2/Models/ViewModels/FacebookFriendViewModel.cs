using System.ComponentModel.DataAnnotations;

namespace TTWebMVCV2.Models.ViewModels
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
