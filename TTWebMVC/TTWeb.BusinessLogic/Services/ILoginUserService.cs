using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface ILoginUserService
    {
        Task<LoginUserModel> GetOrAddUserAsync(LoginUserModel loginUserModel);
        Task<LoginUserModel> GetUserByEmailAsync(string email);
        Task<LoginUserModel> CreateUserAsync(LoginUserModel loginUserModel);
    }
}
