using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace TTWebApi.Models
{
   public class ContextUser
   {
      public int Id { get; set; } = 0;
      public string Email { get; set; }
      public string Firstname { get; set; }
      public string Lastname { get; set; }
      public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();

      public ContextUser()
      {

      }

      public ContextUser(ClaimsPrincipal claimsPrincipal)
      {
         Id = int.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) ? userId : Id;
         Email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
         Firstname = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);
         Lastname = claimsPrincipal.FindFirstValue(ClaimTypes.Surname);
         Roles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(c => c.Value);
      }
   }
}
