using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Entities;
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
        public async Task<FacebookUserModel> Create([FromBody] FacebookUserModel facebookUser)
        {
            return await _facebookUserService.CreateAsync(facebookUser);
        }

        [HttpGet("{id}")]
        public async Task<FacebookUserModel> ReadOne([FromRoute] int id)
        {
            var facebookUser = await _facebookUserService.ReadByIdAsync(id, LoginUserId);
            ThrowExceptionOnUnauthorizedAccess(facebookUser?.OwnerId);
            return facebookUser;
        }

        [HttpGet("")]
        public async Task<IEnumerable<FacebookUserModel>> Read()
        {
            return await _facebookUserService.ReadByOwnerAsync(LoginUserId);
        }

        [HttpPatch("{id}")]
        public async Task<FacebookUserModel> Update([FromRoute] int id, [FromBody] FacebookUserModel updateModel)
        {
            if (id != updateModel.Id) throw new InvalidInputException(nameof(updateModel.Id));

            return await _facebookUserService.UpdateAsync(updateModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _facebookUserService.DeleteAsync(id, LoginUserId);
            return NoContent();
        }
    }
}