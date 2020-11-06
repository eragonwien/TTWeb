using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Account;

namespace TTWeb.BusinessLogic.Services.Authentication
{
    public interface IExternalProviderService
    {
        Task<bool> IsTokenValidAsync(ExternalLoginModel loginModel);
    }
}