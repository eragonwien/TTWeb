using System.ComponentModel.DataAnnotations;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleJobResultModel : BaseEntityModel
    {
        [Required]
        public int ScheduleJobId { get; set; }
    }
}