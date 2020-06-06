using System.ComponentModel.DataAnnotations;

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
