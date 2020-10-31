using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Entities.LoginUser;

namespace TTWeb.BusinessLogic.Services
{
    public interface IExternalAuthenticationService
    {
        Task<bool> IsTokenValidAsync(ExternalLoginModel loginModel);
    }
}
