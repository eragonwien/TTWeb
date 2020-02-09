using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebInterface.Models
{
   [Table("schedulejob")]
   public class ScheduleJob
   {
      public int Id { get; set; }

      [Required]
      public string Name { get; set; }

      [ForeignKey("appuser_id")]
      [Column("appuser_id")]
      public int AppUserId { get; set; }

      public AppUser AppUser { get; set; }

      [ForeignKey("schedulejobtype_id")]
      [Column("schedulejobtype_id")]
      public int ScheduleJobTypeId { get; set; }

      public ScheduleJobType Type { get; set; }

      public virtual ICollection<ScheduleJobParameter> Parameters { get; set; }
   }
}
