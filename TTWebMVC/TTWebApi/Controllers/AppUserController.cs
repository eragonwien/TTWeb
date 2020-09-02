using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TTWebCommon.Models;
using TTWebCommon.Models.Common.Exceptions;
using TTWebCommon.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TTWebApi.Controllers
{
    [Route("appusers")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService userService;

        public AppUserController(IAppUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}", Name = nameof(GetUserByIdAsync))]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await userService.GetUserByIdAsync(id);

            if (user == null)
                throw new WebApiNotFoundException(string.Format("User with ID='{0}' not found", id));

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] AppUser user)
        {
            await userService.CreateUserAsync(user);
            return CreatedAtRoute(nameof(GetUserByIdAsync), new { id = user.Id }, user);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUserAsync(int id, [FromBody] JsonPatchDocument<AppUser> userPatch)
        {
            var user = await userService.GetUserByIdAsync(id);

            if (user == null)
                return BadRequest();

            userPatch.ApplyTo(user);
            await userService.UpdateUserAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task DeleteUserAsync(int id)
        {
            await userService.DeleteUserAsync(id);
        }
    }
}
