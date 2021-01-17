using System;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleTimeFrameMappingModel : ScheduleTimeFrameModel, IHasScheduleIdModel
    {
        public ScheduleTimeFrameMappingModel(int scheduleId, ScheduleTimeFrameModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ScheduleId = scheduleId;
            From = model.From;
            To = model.To;
        }

        public int ScheduleId { get; set; }
    }
}