using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;
using TTWeb.BusinessLogic.Models.Account;

namespace TTWeb.BusinessLogic.Services
{
    public interface IExternalAuthenticationService
    {
        Task<LoginUserModel> ValidateTokenAsync(LoginModel loginModel);
    }
}
