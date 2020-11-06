using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;

namespace TTWeb.Web.Api.Services.Account
{
    public interface IAccountService
    {
        Task<ProcessingResult<LoginUserModel>> AuthenticateExternalAsync(ExternalLoginModel loginModel);
        LoginTokenModel GenerateAccessToken(LoginUserModel user);
        Task<LoginTokenModel> RefreshAccessToken(LoginTokenModel loginTokenModel);
    }
}