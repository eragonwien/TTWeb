using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities.FacebookUser;

namespace TTWeb.BusinessLogic.Services.Facebook
{
    public interface IFacebookUserService
    {
        Task<FacebookUserModel> AddAsync(FacebookUserModel model);
        Task<FacebookUserModel> UpdateAsync(FacebookUserModel model);
        Task DeleteAsync(int id, int loginUserId);
        Task<FacebookUserModel> GetByIdAsync(int id);
        Task<IEnumerable<FacebookUserModel>> GetByLoginUserAsync(int loginUserId);
    }
}
