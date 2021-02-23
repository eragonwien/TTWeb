namespace TTWeb.BusinessLogic.Models
{
    public class ScheduleFacebookUserMappingModel : FacebookUserModel, IHasScheduleIdModel
    {
        public ScheduleFacebookUserMappingModel(int scheduleId, int id)
        {
            ScheduleId = scheduleId;
            Id = id;
        }

        public int ScheduleId { get; set; }
    }
}