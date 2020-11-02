using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities.FacebookUser;

namespace TTWeb.BusinessLogic.Services.FacebookUser
{
    public class FacebookUserService : IFacebookUserService
    {
        public Task<FacebookUserModel> AddAsync(FacebookUserModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<FacebookUserModel> UpdateAsync(FacebookUserModel model, FacebookUserModel existingModel)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<FacebookUserModel> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<FacebookUserModel>> GetByLoginUserAsync(int loginUserId)
        {
            throw new System.NotImplementedException();
        }
    }
}