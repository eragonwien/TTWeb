using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using TTWebApi.Models;
using TTWebApi.Services;
using TTWebCommon.Models;

namespace TTWebApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AppUserController : ControllerBase
   {
      private readonly IAppUserService appUserService;

      public AppUserController(IAppUserService appUserService)
      {
         this.appUserService = appUserService;
      }

      // GET: api/appUser
      [HttpGet]
      public async Task<ActionResult<IEnumerable<AppUser>>> GetAppUserSet()
      {
         return await appUserService.GetAll();
      }

      // GET: api/AppUser/5
      [HttpGet("{id}")]
      public async Task<ActionResult<AppUser>> GetAppUser(int id)
      {
         var appUser = await appUserService.GetOne(id);

         if (appUser == null)
         {
            return NotFound();
         }

         return appUser;
      }

      // POST: api/AppUser
      [HttpPost]
      public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
      {
         await appUserService.Create(appUser);

         return CreatedAtAction(nameof(GetAppUser), new { id = appUser.Id }, appUser);
      }

      [HttpPut("{id}")]
      public async Task<ActionResult<AppUser>> EditAppUser(int id, [FromBody]EditAppUserModel editModel)
      {
         if (editModel == null)
            return BadRequest();

         if (id != editModel.Id)
            ModelState.AddModelError("", "ID does not match");

         var appUser = await appUserService.GetOne(id);
         if (appUser == null)
            ModelState.AddModelError("", string.Format("User {0} not found", id));
               
         if (appUser != null && appUser.Email != editModel.Email && !appUserService.IsEmailAvailable(editModel.Email))
         {
            ModelState.AddModelError(nameof(EditAppUserModel.Email), string.Format("Email {0} is not available", editModel.Email));
         }

         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }
         await appUserService.UpdateProfile(editModel.ToAppUser());
         return Ok(editModel);
      }

      // DELETE: api/AppUser/5
      [HttpDelete("{id}")]
      public async Task<ActionResult<AppUser>> DeleteAppUser(int id)
      {
         var appUser = await appUserService.GetOne(id);
         if (appUser == null)
         {
            return NotFound();
         }
         await appUserService.Remove(appUser);

         return appUser;
      }

      [AllowAnonymous]
      [HttpPost("authenticate")]
      public async Task<IActionResult> Authenticate([FromBody] LoginViewModel loginModel)
      {
         var user = await appUserService.Authenticate(loginModel.Email, loginModel.Password);
         if (user == null)
         {
            return BadRequest();
         }
         return Ok(user);
      }

      [AllowAnonymous]
      [HttpPost("reauthenticate")]
      public async Task<IActionResult> Reauthenticate([FromBody] ReauthenticationViewModel model)
      {
         var user = await appUserService.Reauthenticate(model.AccessToken, model.RefreshToken);
         if (user == null)
         {
            return BadRequest();
         }
         return Ok(user);
      }

      [HttpPost("ping")]
      public IActionResult Ping()
      {
         return Ok();
      }
   }
}
