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

        public ScheduleIntervalType IntervalType { get; set; }

        public ScheduleSenderModel Sender { get; set; }

        public List<ScheduleReceiverModel> Receivers { get; set; }

        public List<DayOfWeek> Weekdays { get; set; }

        public List<ScheduleTimeFrameModel> TimeFrames { get; set; }
    }
}
