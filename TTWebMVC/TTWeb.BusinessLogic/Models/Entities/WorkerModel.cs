using System.Collections.Generic;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class WorkerModel : BaseEntityModel
    {
        public string Secret { get; set; }

        public ICollection<UserPermission> Permissions { get; set; }

        public WorkerModel()
        {
        }

        public WorkerModel(int id, string secret)
        {
            Id = id;
            Secret = secret;
        }
    }
}