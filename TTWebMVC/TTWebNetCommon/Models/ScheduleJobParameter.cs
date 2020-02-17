using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebNetCommon.Models
{
   [Table("schedulejobparameter")]
   public class ScheduleJobParameter
   {
      public int Id { get; set; }

      [Required] 
      public string Value { get; set; }

      [Column("schedulejob_id")]
      public int ScheduleJobId { get; set; }

      public virtual ScheduleJob Job { get; set; }

      [Column("schedulejobparametertype_id")]
      public int TypeId { get; set; }

      public virtual ScheduleJobParameterType Type { get; set; }
   }
}
