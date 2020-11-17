using System.ComponentModel.DataAnnotations;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleJobStatusModel : BaseUserOwnedModel
    {
        [Required]
        public int ScheduleId { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [Required]
        public ProcessingStatus Status { get; set; }
    }
}