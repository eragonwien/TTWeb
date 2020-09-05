using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TTWebCommon.Models;
using TTWebCommon.Models.Common.Exceptions;
using TTWebCommon.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TTWebApi.Controllers
{
    [Route("appusers")]
    public class AppUserController : BaseController
    {
        private readonly IAppUserService userService;
        private readonly IExceptionService exceptionService;

        public AppUserController(IAppUserService userService, IExceptionService exceptionService)
        {
            this.userService = userService;
            this.exceptionService = exceptionService;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] AppUser user)
        {
            await userService.UpdateUserAsync(user);
            return NoContent();
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
