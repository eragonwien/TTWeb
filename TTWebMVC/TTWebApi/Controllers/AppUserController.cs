using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTWebCommon.Models;

namespace TTWebApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AppUserController : ControllerBase
   {
      private readonly TTWebDbContext _context;

      public AppUserController(TTWebDbContext context)
      {
         _context = context;
      }

      // GET: api/AppUser
      [HttpGet]
      public async Task<ActionResult<IEnumerable<AppUser>>> GetAppUserSet()
      {
         return await _context.AppUserSet.ToListAsync();
      }

      // GET: api/AppUser/5
      [HttpGet("{id}")]
      public async Task<ActionResult<AppUser>> GetAppUser(int id)
      {
         var appUser = await _context.AppUserSet.FindAsync(id);

         if (appUser == null)
         {
            return NotFound();
         }

         return appUser;
      }

      // PUT: api/AppUser/5
      [HttpPut("{id}")]
      public async Task<IActionResult> PutAppUser(int id, AppUser appUser)
      {
         if (id != appUser.Id)
         {
            return BadRequest();
         }

         _context.Entry(appUser).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!AppUserExists(id))
            {
               return NotFound();
            }
            else
            {
               throw;
            }
         }

         return NoContent();
      }

      // POST: api/AppUser
      [HttpPost]
      public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
      {
         _context.AppUserSet.Add(appUser);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetAppUser", new { id = appUser.Id }, appUser);
      }

      // DELETE: api/AppUser/5
      [HttpDelete("{id}")]
      public async Task<ActionResult<AppUser>> DeleteAppUser(int id)
      {
         var appUser = await _context.AppUserSet.FindAsync(id);
         if (appUser == null)
         {
            return NotFound();
         }

         _context.AppUserSet.Remove(appUser);
         await _context.SaveChangesAsync();

         return appUser;
      }

      private bool AppUserExists(int id)
      {
         return _context.AppUserSet.Any(e => e.Id == id);
      }
   }
}
