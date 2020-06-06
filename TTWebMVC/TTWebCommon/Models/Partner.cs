using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTWebCommon.Models
{
   [Table("partner")]
   public class Partner
   {
      public int Id { get; set; }
      [Column("appuser_id")]
      public int AppUserId { get; set; }
      public AppUser AppUser { get; set; }
      public string Name { get; set; }
      [Column("facebook_user")]
      public string FacebookUser { get; set; }
      public virtual ICollection<ScheduleJobPartner> ScheduleJobPartners { get; set; }
   }
}
