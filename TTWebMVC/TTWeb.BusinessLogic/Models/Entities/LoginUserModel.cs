using System.Collections.Generic;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class LoginUserModel : BaseEntityModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}