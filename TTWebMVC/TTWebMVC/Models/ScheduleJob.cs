using System.Collections.Generic;

namespace TTWebMVC.Models
{
   public class ScheduleJob
   {
      public long Id { get; set; }
      public AppUser AppUser { get; set; }
      public ScheduleJobDefinition Definition { get; set; }
      public List<ScheduleJobParameter> Parameters { get; set; } = new List<ScheduleJobParameter>();
   }
}