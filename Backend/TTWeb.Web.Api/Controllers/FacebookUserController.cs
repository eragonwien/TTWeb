using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models;
using TTWeb.BusinessLogic.Services;

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
            var facebookUser = await _facebookUserService.ReadByIdAsync(id, OwnerId);
            ThrowExceptionOnWrongOwner(facebookUser.OwnerId);
            return facebookUser;
        }

        [HttpGet("")]
        [Authorize(Policy = Startup.RequireAccessAllResourcesPermissionPolicy)]
        public async Task<IEnumerable<FacebookUserModel>> Read()
        {
            return await _facebookUserService.Read();
        }

        [HttpPatch("{id}")]
        public async Task<FacebookUserModel> Update([FromRoute] int id, [FromBody] FacebookUserModel updateModel)
        {
            if (id != updateModel.Id) throw new InvalidInputException(nameof(updateModel.Id));
            ThrowExceptionOnWrongOwner(updateModel.OwnerId);

            return await _facebookUserService.UpdateAsync(updateModel);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _facebookUserService.DeleteAsync(id, OwnerId);
        }
    }
}