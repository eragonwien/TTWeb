using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTWebCommon.Models
{
   [Table("LoginUser")]
   public class LoginUser
   {
      [Required]
      public int Id { get; set; }
      [Required]
      [DataType(DataType.EmailAddress)]
      public string Email { get; set; }
      [Required]
      [JsonIgnore]
      [DataType(DataType.Password)]
      public string Password { get; set; }
      public string Title { get; set; }
      public string Firstname { get; set; }
      public string Lastname { get; set; }
      [NotMapped]
      public string AccessToken { get; set; }
      [Column("refresh_token")]
      public string RefreshToken { get; set; }
      [Column("disabled")]
      public int DisabledFlag { get; set; }
      [NotMapped]
      public bool Disabled { get; set; }

      public virtual ICollection<AppUser> AppUsers { get; set; }
      public virtual ICollection<LoginUserRoleMapping> LoginUserRolesMapping { get; set; }
   }
}
