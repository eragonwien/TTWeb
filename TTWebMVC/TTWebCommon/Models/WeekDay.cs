using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTWebCommon.Models
{
   [Table("weekday")]
   public class WeekDay
   {
      public int Id { get; set; }
      public DayOfWeek Name { get; set; }

      public virtual ICollection<JobWeekDay> JobWeekDays { get; set; }
   }
}
