using System;
using System.Collections.Generic;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models
{
    public class ScheduleModel : BaseUserOwnedModel
    {
        public ScheduleAction Action { get; set; }

        public ScheduleIntervalType IntervalType { get; set; }

        public FacebookUserModel Sender { get; set; }

        public IEnumerable<FacebookUserModel> Receivers { get; set; }

        public List<DayOfWeek> Weekdays { get; set; }

        public List<ScheduleTimeFrameModel> TimeFrames { get; set; }

        public ProcessingStatus PlanningStatus { get; set; }
    }
}