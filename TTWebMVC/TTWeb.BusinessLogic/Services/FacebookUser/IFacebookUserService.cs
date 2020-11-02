using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities.FacebookUser;

namespace TTWeb.BusinessLogic.Services.FacebookUser
{
    public interface IFacebookUserService
    {
        Task<FacebookUserModel> AddAsync(FacebookUserModel model);
        Task<FacebookUserModel> UpdateAsync(FacebookUserModel model, FacebookUserModel existingModel);
        Task DeleteAsync(int id);
        Task<FacebookUserModel> GetByIdAsync(int id);
        Task<IEnumerable<FacebookUserModel>> GetByLoginUserAsync(int loginUserId);
    }
}
