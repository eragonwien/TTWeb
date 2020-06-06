using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTWebCommon.Models
{
   [Table("schedulejobdetail")]
   public class ScheduleJobDetail
   {
      public int Id { get; set; }
      [Column("schedulejobdef_id")]
      public int ScheduleJobDefId { get; set; }
      public ScheduleJobDef JobDef { get; set; }
      [Column("execution_time")]
      public string ExecutionTimeString { get; set; }
      public ScheduleJobStatus Status { get; set; } = ScheduleJobStatus.NEW;

   }
}
