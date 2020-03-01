using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Extensions;
using TTWebCommon.Facebook;

namespace TTWebCommon.Models
{
   [Table("schedulejob")]
   public class ScheduleJob
   {
      public int Id { get; set; }

      [Required]
      public string Name { get; set; }

      [Column("appuser_id")]
      public int AppUserId { get; set; }

      public AppUser AppUser { get; set; }

      [Column("schedulejobtype_id")]
      public int ScheduleJobTypeId { get; set; }

      public ScheduleJobType Type { get; set; }

      public virtual ICollection<ScheduleJobParameter> Parameters { get; set; }

      [Column("planned_date")]
      public DateTimeOffset? PlannedDate { get; set; }

      public FacebookServiceParameter ToFacebookParameters()
      {
         var parameter = new FacebookServiceParameter
         {
            Email = AppUser.Email,
            Password = AppUser.Password,
            ActionType = (FacebookServiceActionType)Enum.Parse(typeof(FacebookServiceActionType), Type.Name.ToUpper())
         };
         parameter = Parameters.ToDictionary(p => p.Type.Name, p => p.Value).Map(parameter);
         return parameter;
      }
   }
}
