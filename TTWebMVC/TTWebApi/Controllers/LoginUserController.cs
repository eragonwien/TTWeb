using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTWebApi.Services;
using TTWebCommon.Models;

namespace TTWebApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class LoginUserController : ControllerBase
   {
      private readonly ILoginUserService loginUserService;

      public LoginUserController(ILoginUserService loginUserService)
      {
         this.loginUserService = loginUserService;
      }

      // GET: api/LoginUser
      [HttpGet]
      public async Task<ActionResult<IEnumerable<LoginUser>>> GetLoginUserSet()
      {
         return await loginUserService.GetAll();
      }

      // GET: api/LoginUser/5
      [HttpGet("{id}")]
      public async Task<ActionResult<LoginUser>> GetLoginUser(int id)
      {
         var loginUser = await loginUserService.GetOne(id);

         if (loginUser == null)
         {
            return NotFound();
         }

         return loginUser;
      }

      // POST: api/LoginUser
      [HttpPost]
      public async Task<ActionResult<LoginUser>> PostLoginUser(LoginUser loginUser)
      {
         await loginUserService.Create(loginUser);

         return CreatedAtAction(nameof(GetLoginUser), new { id = loginUser.Id }, loginUser);
      }

      // DELETE: api/LoginUser/5
      [HttpDelete("{id}")]
      public async Task<ActionResult<LoginUser>> DeleteLoginUser(int id)
      {
         var loginUser = await loginUserService.GetOne(id);
         if (loginUser == null)
         {
            return NotFound();
         }
         await loginUserService.Remove(loginUser);

         return loginUser;
      }

      [AllowAnonymous]
      [HttpPost("authenticate")]
      public async Task<IActionResult> Authenticate(string email, string password)
      {
         var user = await loginUserService.Authenticate(email, password);
         if (user == null)
         {
            return BadRequest();
         }
         return Ok(user);
      }

      [AllowAnonymous]
      [HttpPost("reauthenticate")]
      public async Task<IActionResult> Reauthenticate(string accessToken, string refreshToken)
      {
         var user = await loginUserService.Reauthenticate(accessToken, refreshToken);
         if (user == null)
         {
            return BadRequest();
         }
         return Ok(user);
      }
   }
}
