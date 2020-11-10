using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleModel : BaseUserOwnedModel, IValidatableObject
    {
        [Required]
        public ScheduleAction Action { get; set; }

        [Required]
        public ScheduleIntervalType IntervalType { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public List<ScheduleReceiverModel> Receivers { get; set; }

        [Required]
        public List<DayOfWeek> Weekdays { get; set; }

        [Required]
        public List<ScheduleTimeFrameModel> TimeFrames { get; set; }

        public ProcessingStatus PlanningStatus { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            yield break;
        }
    }
}