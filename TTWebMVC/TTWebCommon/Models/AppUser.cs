﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebCommon.Models
{
   [Table("AppUser")]
   public class AppUser
   {
      public int Id { get; set; }
      [Required]
      public string Email { get; set; }
      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }
      [Required]
      public string Firstname { get; set; }
      [Required]
      public string Lastname { get; set; }

      [Column("loginuser_id")]
      public int LoginUserId { get; set; }

      public LoginUser LoginUser { get; set; }

      public virtual ICollection<ScheduleJob> ScheduleJobs { get; set; }
   }
}
