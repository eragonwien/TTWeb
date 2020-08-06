using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebCommon.Models
{
   public class ScheduleJobDetail
   {
      public int Id { get; set; }
      public int ScheduleJobDefId { get; set; }
      public ScheduleJobDef JobDef { get; set; }
      public string ExecutionTimeString { get; set; }
      public ScheduleJobStatus Status { get; set; } = ScheduleJobStatus.NEW;

        public static ScheduleJobDetail Calculate(ScheduleJobDef d)
        {
            throw new NotImplementedException();
        }
    }
}
