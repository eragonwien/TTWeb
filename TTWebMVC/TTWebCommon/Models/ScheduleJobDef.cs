using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Extensions;
using TTWebCommon.Facebook;

namespace TTWebCommon.Models
{
   [Table("schedulejobdef")]
   public class ScheduleJobDef
   {
      public int Id { get; set; }

      [Required]
      public string Name { get; set; }

      [Column("appuser_id")]
      public int AppUserId { get; set; }

      public AppUser AppUser { get; set; }

      [Column("type")]
      public ScheduleJobType Type { get; set; }
      [Column("interval_type")]
      public IntervalType IntervalType { get; set; }
      [Column("time_from")]
      public string TimeFrom { get; set; }
      [Column("time_to")]
      public string TimeTo { get; set; }
      [Column("time_offset")]
      public string TimeOffset { get; set; }
      [Column("active")]
      public int ActiveFlag { get; set; } = 0;
      [NotMapped]
      public bool Active {
         get
         {
            return ActiveFlag == 1;
         }
      }

      public virtual ICollection<JobWeekDay> JobWeekDays { get; set; }
      public virtual ICollection<ScheduleJobPartner> ScheduleJobPartners { get; set; }
      public virtual ICollection<ScheduleJobDetail> JobDetails { get; set; }
   }
}
