using System.Collections.Generic;

namespace TTWeb.Data.Models
{
    public class ScheduleJob : IHasIdEntity
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public ICollection<ScheduleJobResult> Results { get; set; }
    }
}