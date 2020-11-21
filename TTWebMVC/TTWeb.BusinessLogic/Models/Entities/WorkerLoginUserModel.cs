using System.Collections.Generic;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class WorkerLoginUserModel : BaseEntityModel
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}