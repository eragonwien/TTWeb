using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;
using TTWeb.BusinessLogic.Models.Account;

namespace TTWeb.BusinessLogic.Services
{
    public class ExternalAuthenticationService : IExternalAuthenticationService
    {
        public Task<LoginUserModel> ValidateTokenAsync(LoginModel loginModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
