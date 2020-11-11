using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleModel : BaseUserOwnedModel
    {
        [Required]
        public ScheduleAction Action { get; set; }

        [Required]
        public ScheduleIntervalType IntervalType { get; set; }

        [Required]
        public FacebookUser Sender { get; set; }

        [Required]
        public IEnumerable<FacebookUserModel> Receivers { get; set; }

        [Required]
        public List<DayOfWeek> Weekdays { get; set; }

        [Required]
        public List<ScheduleTimeFrameModel> TimeFrames { get; set; }

        public ProcessingStatus PlanningStatus { get; set; }

        public int? WorkerId { get; set; }
    }
}