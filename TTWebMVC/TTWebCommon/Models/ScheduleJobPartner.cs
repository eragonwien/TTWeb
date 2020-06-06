using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTWebCommon.Models
{
   [Table("schedulejobpartner")]
   public class ScheduleJobPartner
   {
      [Column("schedulejobdef_id")]
      public int ScheduleJobDefId { get; set; }
      public ScheduleJobDef ScheduleJobDef { get; set; }
      [Column("partner_id")]
      public int PartnerId { get; set; }
      public Partner Partner { get; set; }
   }
}
