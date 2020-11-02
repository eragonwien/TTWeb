using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Account;

namespace TTWeb.BusinessLogic.Services
{
    public interface IExternalAuthenticationService
    {
        Task<bool> IsTokenValidAsync(ExternalLoginModel loginModel);
    }
}
