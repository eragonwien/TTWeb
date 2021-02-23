namespace TTWeb.BusinessLogic.Models
{
    public class ScheduleJobResultModel : BaseEntityModel
    {
        public int ScheduleJobId { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
    }
}