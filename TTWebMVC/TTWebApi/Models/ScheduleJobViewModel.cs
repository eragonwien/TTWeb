using System;
using System.Collections.Generic;
using System.Linq;

namespace TTWebApi.Models
{
   public class ScheduleJobViewModel
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public DateTime? From { get; set; }
      public DateTime? To { get; set; }
      public IEnumerable<DayOfWeek> SelectedDays { get; set; } = Enumerable.Empty<DayOfWeek>();
   }
}
