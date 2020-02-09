using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebInterface.Models
{
   public class ScheduleJobParameter
   {
      public int Id { get; set; }
      [Required] 
      public string Value { get; set; }

      [ForeignKey("schedulejob_id")]
      public virtual ScheduleJob Job { get; set; }
      [ForeignKey("schedulejobparametertype_id")]
      public virtual ScheduleJobParameterType Type { get; set; }
   }
}
