using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface IFacebookUserService
    {
        Task<FacebookUserModel> CreateAsync(FacebookUserModel model);

        Task<FacebookUserModel> UpdateAsync(FacebookUserModel model);

        Task DeleteAsync(int id, int? ownerId);

        Task<FacebookUserModel> ReadByIdAsync(int id, int? ownerId);

        Task<IEnumerable<FacebookUserModel>> Read();
    }
}