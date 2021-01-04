using System;
using System.Collections.Generic;
using System.Linq;

namespace TTWeb.Data.Models
{
    public class Worker : IHasIdEntity
    {
        public int Id { get; set; }

        public string Secret { get; set; }

        public DateTime CreateAt { get; set; }

        public ICollection<WorkerPermissionMapping> WorkerPermissionMappings { get; set; }

        public ICollection<Schedule> WorkingSchedules { get; set; }

        public Worker CreatedAt(DateTime dateTime)
        {
            CreateAt = dateTime;
            return this;
        }

        public Worker WithSecret(string secret)
        {
            Secret = secret;
            return this;
        }

        public Worker WithDefaultPermissions()
        {
            if (!WorkerPermissionMappings.Any(m => m.UserPermission == UserPermission.IsWorker))
                WorkerPermissionMappings.Add(new WorkerPermissionMapping { UserPermission = UserPermission.IsWorker });

            if (!WorkerPermissionMappings.Any(m => m.UserPermission == UserPermission.AccessOwnResources))
                WorkerPermissionMappings.Add(new WorkerPermissionMapping { UserPermission = UserPermission.AccessOwnResources });

            return this;
        }
    }
}
