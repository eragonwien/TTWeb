using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebInterface.Models
{
   [Table("schedulejobparameter")]
   public class ScheduleJobParameter
   {
      public int Id { get; set; }

      [Required] 
      public string Value { get; set; }

      [ForeignKey("schedulejob_id")]
      [Column("schedulejob_id")]
      public int ScheduleJobId { get; set; }

      public virtual ScheduleJob Job { get; set; }

      [ForeignKey("schedulejobparametertype_id")]
      [Column("schedulejobparametertype_id")]
      public int TypeId { get; set; }

      public virtual ScheduleJobParameterType Type { get; set; }
   }
}
