using System;
using System.Collections.Generic;

namespace TTWeb.Data.Models
{
    public class Worker : IHasIdEntity
    {
        public int Id { get; set; }

        public string Secret { get; set; }

        public DateTime CreateAt { get; set; }

        public ICollection<WorkerPermissionMapping> WorkerPermissionMappings { get; set; }

        public ICollection<Schedule> WorkingSchedules { get; set; }

        public Worker()
        {
        }

        public Worker(string secret)
        {
            Secret = secret;
            CreateAt = DateTime.UtcNow;
            WorkerPermissionMappings = new List<WorkerPermissionMapping>
            {
                new WorkerPermissionMapping { UserPermission = UserPermission.IsWorker },
                new WorkerPermissionMapping { UserPermission = UserPermission.AccessOwnResources }
            };
        }
    }
}
