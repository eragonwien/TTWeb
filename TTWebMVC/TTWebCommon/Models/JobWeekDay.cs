using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTWebCommon.Models
{
   [Table("jobweekday")]
   public class JobWeekDay
   {
      [Column("schedulejobdef_id")]
      public int ScheduleJobDefId { get; set; }
      public ScheduleJobDef ScheduleJobDef { get; set; }
      [Column("weekday_id")]
      public int WeekDayId { get; set; }
      public WeekDay WeekDay { get; set; }
   }
}
