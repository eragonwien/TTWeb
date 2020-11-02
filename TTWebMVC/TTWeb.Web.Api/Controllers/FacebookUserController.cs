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
        public async Task<IActionResult> Add([FromBody] FacebookUserModel facebookUser)
        {
            if (!ModelState.IsValid) throw new InvalidInputException(ModelState);

            facebookUser = await _facebookUserService.AddAsync(facebookUser);

            return Ok(facebookUser);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] FacebookUserModel updateModel)
        {
            if (!ModelState.IsValid) throw new InvalidInputException(ModelState);
            if (id != updateModel.Id) throw new InvalidInputException(nameof(updateModel.Id));
            ThrowExceptionOnUnauthorizedAccess(updateModel.LoginUserId);

            var facebookUser = await _facebookUserService.UpdateAsync(updateModel);

            return Ok(facebookUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) throw new InvalidInputException(ModelState);

            await _facebookUserService.DeleteAsync(id, LoginUserId);

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