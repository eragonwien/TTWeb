using System.Collections.Generic;
using System.Security.Claims;
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

        public IEnumerable<Claim> GenerateClaims()
        {
            yield return new Claim(ClaimTypes.NameIdentifier, Id.ToString());
            yield return new Claim(ClaimTypes.Email, Email);
            yield return new Claim(ClaimTypes.GivenName, FirstName);
            yield return new Claim(ClaimTypes.Surname, LastName);

            foreach (var permission in UserPermissions)
                yield return new Claim(ClaimTypes.Role, permission.ToString());
        }
    }
}