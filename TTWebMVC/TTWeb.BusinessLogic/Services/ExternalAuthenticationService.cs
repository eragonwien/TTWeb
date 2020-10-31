using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Account;

namespace TTWeb.BusinessLogic.Services
{
    public class ExternalAuthenticationService : IExternalAuthenticationService
    {
#pragma warning disable CS1998 // TODO: adds facebook token validation
        public async Task<bool> IsTokenValidAsync(ExternalLoginModel loginModel)
#pragma warning restore CS1998
        {
            if (loginModel is null) throw new System.ArgumentNullException(nameof(loginModel));

#if DEBUG
            return true;
#endif
        }
    }
}
