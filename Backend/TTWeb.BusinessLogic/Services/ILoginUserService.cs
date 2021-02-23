using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface ILoginUserService
    {
        Task<LoginUserModel> GetByIdAsync(int id);

        Task<LoginUserModel> GetByEmailAsync(string email);

        Task<LoginUserModel> CreateAsync(LoginUserModel loginUserModel);
    }
}