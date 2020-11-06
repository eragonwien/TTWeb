using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Account;

namespace TTWeb.BusinessLogic.Services.Authentication
{
    public interface IAuthenticationHelperService
    {
        Task<bool> IsExternalAccessTokenValidAsync(ExternalLoginModel loginModel);
    }
}