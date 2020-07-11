using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Extensions;
using TTWebCommon.Facebook;

namespace TTWebCommon.Models
{
   public class ScheduleJobDef
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public int AppUserId { get; set; }
      public AppUser AppUser { get; set; }
      public ScheduleJobType Type { get; set; }
      public IntervalTypeEnum IntervalType { get; set; }
      public string TimeFrom { get; set; }
      public string TimeTo { get; set; }
      public string TimeZone { get; set; }
      public bool Active { get; set; }
   }
}
