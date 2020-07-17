using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebMVCV2.Models
{
   public class ScheduleDefViewModel
   {
      [StringLength(16)]
      public string Name { get; set; }
      [Required]
      [Display(Name = "Friend")]
      public int? FriendId { get; set; }
      [Required]
      [Display(Name = "Login")]
      public int? FacebookCredentialId { get; set; }
      [Required]
      public ScheduleJobType Type { get; set; }
      [Required]
      [Display(Name = "Interval")]
      public IntervalTypeEnum InternvalType { get; set; }
      [Required]
      public DateTime? TimeFrom { get; set; }
      [Required]
      public DateTime? TimeTo { get; set; }
      [Required]
      public string TimeZone { get; set; }
      [Required]
      public bool Active { get; set; }

      public static ScheduleDefViewModel Default = new ScheduleDefViewModel();

      public ScheduleJobDef ToScheduleJobDef(int appuserId)
      {
         return new ScheduleJobDef
         {
            Id = -1,
            Name = Name,
            AppUserId = appuserId,
            FriendId = FriendId.Value,
            FacebookCredentialId = FacebookCredentialId.Value,
            Type = Type,
            IntervalType = InternvalType,
            TimeFrom = TimeFrom != null && TimeFrom.HasValue ? TimeFrom.Value.ToString("HH:mm") : null,
            TimeTo = TimeTo != null && TimeTo.HasValue ? TimeTo.Value.ToString("HH:mm") : null,
            TimeZone = TimeZone,
            Active = Active,
         };
      }
   }
}
