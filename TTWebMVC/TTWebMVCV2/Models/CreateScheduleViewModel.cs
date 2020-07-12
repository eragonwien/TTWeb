using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebMVCV2.Models
{
   public class CreateScheduleViewModel
   {
      [StringLength(16)]
      public string Name { get; set; }
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

      public ScheduleJobDef ToScheduleJobDef(int appUserId)
      {
         return new ScheduleJobDef
         {
            Id = -1,
            AppUserId = appUserId,
            Name = Name,
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
