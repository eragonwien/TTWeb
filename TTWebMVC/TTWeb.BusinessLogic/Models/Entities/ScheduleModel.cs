using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleModel : BaseUserOwnedModel
    {
        public ScheduleAction Action { get; set; }
        public ScheduleIntervalType IntervalType { get; set; }
        public int SenderId { get; set; }
    }
}