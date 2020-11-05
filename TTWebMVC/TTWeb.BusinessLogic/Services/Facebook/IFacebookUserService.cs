using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services.Facebook
{
    public interface IFacebookUserService
    {
        Task<FacebookUserModel> CreateAsync(FacebookUserModel model);
        Task<FacebookUserModel> UpdateAsync(FacebookUserModel model);
        Task DeleteAsync(int id, int ownerId);
        Task<FacebookUserModel> ReadByIdAsync(int id, int ownerId);
        Task<IEnumerable<FacebookUserModel>> ReadByOwnerAsync(int ownerId);
    }
}
