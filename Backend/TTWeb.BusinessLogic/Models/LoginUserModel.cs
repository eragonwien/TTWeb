using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Models
{
    public class LoginUserModel : BaseEntityModel
    {
        [Required] public string Email { get; set; }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        public ICollection<UserPermission> Permissions { get; set; }
    }
}