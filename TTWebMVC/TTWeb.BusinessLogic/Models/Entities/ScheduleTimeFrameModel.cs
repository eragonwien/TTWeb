using System;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleTimeFrameModel : BaseEntityModel
    {
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}