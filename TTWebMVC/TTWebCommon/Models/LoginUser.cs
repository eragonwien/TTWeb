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
      [DataType(DataType.Password)]
      public string Password { get; set; }
      public string Title { get; set; }
      public string Firstname { get; set; }
      public string Lastname { get; set; }
      [NotMapped]
      public string AccessToken { get; set; }
      public string RefreshToken { get; set; }
      [Column("loginuserrole_id")]
      public int LoginUserRoleId { get; set; }
      public virtual ICollection<AppUser> AppUsers { get; set; }
      public virtual LoginUserRole LoginUserRole { get; set; }
   }
}
