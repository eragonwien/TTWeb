using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTWebCommon.Models
{
   [Table("AppUser")]
   public class AppUser
   {
      [Required]
      public int Id { get; set; }
      [Required]
      [DataType(DataType.EmailAddress)]
      public string Email { get; set; }
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
      [Column("disabled", TypeName = "bit")]
      [DefaultValue(false)]
      public bool Disabled { get; set; }
      [Column("active", TypeName = "bit")]
      [DefaultValue(false)]
      public bool Active { get; set; }
      [Column("facebook_user")]
      public string FacebookUser { get; set; }
      [Column("facebook_password")]
      [JsonIgnore]
      public string FacebookPassword { get; set; }
      [JsonIgnore]
      public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
      [JsonIgnore]
      public virtual ICollection<ScheduleJobDef> ScheduleJobDefs { get; set; }

      public AppUser ClearPassword()
      {
         Password = null;
         return this;
      }
   }
}
