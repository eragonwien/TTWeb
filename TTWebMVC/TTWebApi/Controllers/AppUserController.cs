using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTWebCommon.Models;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await userService.GetOne(id);
            if (user == null)
            {
                return NotFound(id);
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task PostAsync([FromBody] AppUser user)
        {
            await userService.Create(user);
        }

        [HttpPut("{id}")]
        public async Task PutAsync([FromBody] AppUser user)
        {
            await userService.UpdateProfile(user);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await userService.Remove(id);
        }
    }
}
