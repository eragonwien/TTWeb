using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleJobModel : BaseUserOwnedModel
    {
        [Required]
        public int ScheduleId { get; set; }

        public int WorkerId { get; set; }

        public ScheduleAction Action { get; set; }

        public DateTime StartTime { get; set; }
        public ScheduleFacebookUserModel Sender { get; set; }
        public IEnumerable<ScheduleFacebookUserModel> Receivers { get; set; }
    }
}
