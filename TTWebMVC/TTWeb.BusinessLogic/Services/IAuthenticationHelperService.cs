using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Account;

namespace TTWeb.BusinessLogic.Services
{
    public interface IAuthenticationHelperService
    {
        Task<bool> IsTokenValidAsync(ExternalLoginModel loginModel);
    }
}