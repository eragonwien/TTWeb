using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services.LoginUser
{
    public interface ILoginUserService
    {
        Task<LoginUserModel> GetByIdAsync(int id);

        Task<LoginUserModel> GetByEmailAsync(string email);

        Task<LoginUserModel> CreateAsync(LoginUserModel loginUserModel);
    }
}