using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TTWebApi.Models;
using TTWebApi.Services;
using TTWebCommon.Models;

namespace TTWebApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AppUserController : BaseController
   {
      private readonly IAccountService accService;
      private readonly IAppUserService appUserService;

      public AppUserController(IAccountService accService, IAppUserService appUserService)
      {
         this.accService = accService;
         this.appUserService = appUserService;
      }

      // GET: api/AppUser/5
      [HttpGet("{id}")]
      public async Task<ActionResult<AppUser>> GetAppUser(int id)
      {
         if (ContextUser.Id != id)
         {
            return NotFound();
         }
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
      public async Task<ActionResult<AppUser>> EditAppUser(int id, [FromBody] AppUser appUser)
      {
         if (appUser == null)
            return BadRequest();

         if (id != appUser.Id)
            ModelState.AddModelError("", "ID does not match");

         if (!await appUserService.Exist(id))
            ModelState.AddModelError("", string.Format("User {0} not found", id));

         if (!await appUserService.IsEmailAvailable(appUser.Email, appUser.Id))
         {
            ModelState.AddModelError(nameof(AppUser.Email), string.Format("Email {0} is not available", appUser.Email));
         }

         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }
         await appUserService.UpdateProfile(appUser);
         return Ok(appUser);
      }

      // DELETE: api/AppUser/5
      [HttpDelete("{id}")]
      public async Task<ActionResult<AppUser>> DeleteAppUser(int id)
      {
         await appUserService.Remove(id);
         return NoContent();
      }

      [AllowAnonymous]
      [HttpPost("authenticate")]
      public async Task<IActionResult> Authenticate([FromBody] LoginViewModel loginModel)
      {
         var user = await accService.Authenticate(loginModel.Email, loginModel.Password);
         if (user == null)
         {
            return BadRequest();
         }
         return Ok(user);
      }

      [HttpPost("reauthenticate")]
      public async Task<IActionResult> Reauthenticate([FromBody] ReauthenticationViewModel model)
      {
         var user = await accService.Reauthenticate(model.AccessToken, model.RefreshToken);
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
