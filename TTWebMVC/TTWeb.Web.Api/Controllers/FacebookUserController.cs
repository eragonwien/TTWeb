using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Entities.FacebookUser;
using TTWeb.BusinessLogic.Services.Facebook;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/facebook-users")]
    [ApiController]
    public class FacebookUserController : BaseController
    {
        private readonly IFacebookUserService _facebookUserService;

        public FacebookUserController(IFacebookUserService facebookUserService)
        {
            _facebookUserService = facebookUserService;
        }

        [HttpPost]
        public async Task<FacebookUserModel> Add([FromBody] FacebookUserModel facebookUser)
        {
            if (!ModelState.IsValid) throw new InvalidInputException(ModelState);
            return await _facebookUserService.AddAsync(facebookUser);
        }

        [HttpPatch("{id}")]
        public async Task<FacebookUserModel> Update([FromRoute] int id, [FromBody] FacebookUserModel updateModel)
        {
            if (!ModelState.IsValid) throw new InvalidInputException(ModelState);
            if (id != updateModel.Id) throw new InvalidInputException(nameof(updateModel.Id));
            ThrowExceptionOnUnauthorizedAccess(updateModel.OwnerId);

            return await _facebookUserService.UpdateAsync(updateModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) throw new InvalidInputException(ModelState);

            await _facebookUserService.DeleteAsync(id, LoginUserId);
            return NoContent();
        }

        [HttpGet("{:id}")]
        public async Task<FacebookUserModel> GetOne([FromRoute] int id)
        {
            var facebookUser = await _facebookUserService.GetByIdAsync(id);
            ThrowExceptionOnUnauthorizedAccess(facebookUser?.OwnerId);
            return facebookUser;
        }

        [HttpGet("")]
        public async Task<IEnumerable<FacebookUserModel>> GetAll([FromQuery] int ownerId)
        {
            ThrowExceptionOnUnauthorizedAccess(ownerId);
            return await _facebookUserService.GetByOwnerAsync(ownerId);
        }
    }
}