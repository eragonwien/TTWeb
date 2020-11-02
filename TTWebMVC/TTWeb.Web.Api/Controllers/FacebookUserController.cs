using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Entities.FacebookUser;
using TTWeb.BusinessLogic.Services.FacebookUser;

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
        public async Task<IActionResult> Add([FromBody] FacebookUserModel facebookUser)
        {
            if (!ModelState.IsValid) throw new InvalidInputException(ModelState);

            // TODO: validates and throws exception inside addAsync
            facebookUser = await _facebookUserService.AddAsync(facebookUser);

            return Ok(facebookUser);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] FacebookUserModel updateModel)
        {
            if (!ModelState.IsValid) throw new InvalidInputException(ModelState);

            // TODO: moves this block inside updateAsync
            var facebookUser = await _facebookUserService.GetByIdAsync(id);
            if (facebookUser == null) throw new ResourceNotFoundException(nameof(facebookUser), id.ToString());
            ThrowExceptionOnUnauthorizedAccess(facebookUser.LoginUserId);

            facebookUser = await _facebookUserService.UpdateAsync(updateModel, facebookUser);

            return Ok(facebookUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) throw new InvalidInputException(ModelState);

            // TODO: moves this block inside deleteAsync
            var facebookUser = await _facebookUserService.GetByIdAsync(id);
            if (facebookUser == null) throw new ResourceNotFoundException(nameof(facebookUser), id.ToString());
            ThrowExceptionOnUnauthorizedAccess(facebookUser.LoginUserId);

            await _facebookUserService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("{:id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var facebookUser = await _facebookUserService.GetByIdAsync(id);
            ThrowExceptionOnUnauthorizedAccess(facebookUser?.LoginUserId);
            return Ok(facebookUser);
        }
    }
}