using System.Collections.Generic;

namespace TTWeb.Data.Models
{
    public class Worker : IHasIdEntity
    {
        public int Id { get; set; }

        public string Secret { get; set; }

        public ICollection<WorkerPermissionMapping> WorkerPermissionMappings { get; set; }

        public ICollection<Schedule> WorkingSchedules { get; set; }
    }
}
