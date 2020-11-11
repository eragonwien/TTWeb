namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleFacebookUserMappingModel : FacebookUserModel, IHasScheduleIdModel
    {
        public int ScheduleId { get; set; }

        public ScheduleFacebookUserMappingModel(int scheduleId, int id)
        {
            ScheduleId = scheduleId;
            Id = id;
        }
    }
}