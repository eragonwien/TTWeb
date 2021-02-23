using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;

namespace TTWeb.Web.Api.Services
{
    public interface IAccountService
    {
        Task<ProcessingResult<LoginUserModel>> AuthenticateExternalAsync(ExternalLoginModel loginModel);
        LoginTokenModel GenerateAccessToken(LoginUserModel user);
        Task<LoginTokenModel> RefreshAccessToken(LoginTokenModel loginTokenModel);
    }
}