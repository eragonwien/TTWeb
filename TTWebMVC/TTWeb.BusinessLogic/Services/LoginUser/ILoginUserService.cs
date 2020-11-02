using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities.LoginUser;

namespace TTWeb.BusinessLogic.Services.LoginUser
{
    public interface ILoginUserService
    {
        Task<LoginUserModel> GetByEmailAsync(string email);
        Task<LoginUserModel> CreateAsync(LoginUserModel loginUserModel);
    }
}