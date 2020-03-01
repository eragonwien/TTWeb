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
   public class LoginUserController : ControllerBase
   {
      private readonly TTWebDbContext _context;

      public LoginUserController(TTWebDbContext context)
      {
         _context = context;
      }

      // GET: api/LoginUser
      [HttpGet]
      public async Task<ActionResult<IEnumerable<LoginUser>>> GetLoginUserSet()
      {
         return await _context.LoginUserSet.ToListAsync();
      }

      // GET: api/LoginUser/5
      [HttpGet("{id}")]
      public async Task<ActionResult<LoginUser>> GetLoginUser(int id)
      {
         var loginUser = await _context.LoginUserSet.FindAsync(id);

         if (loginUser == null)
         {
            return NotFound();
         }

         return loginUser;
      }

      // PUT: api/LoginUser/5
      [HttpPut("{id}")]
      public async Task<IActionResult> PutLoginUser(int id, LoginUser loginUser)
      {
         if (id != loginUser.Id)
         {
            return BadRequest();
         }

         _context.Entry(loginUser).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!LoginUserExists(id))
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

      // POST: api/LoginUser
      [HttpPost]
      public async Task<ActionResult<LoginUser>> PostLoginUser(LoginUser loginUser)
      {
         _context.LoginUserSet.Add(loginUser);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetLoginUser", new { id = loginUser.Id }, loginUser);
      }

      // DELETE: api/LoginUser/5
      [HttpDelete("{id}")]
      public async Task<ActionResult<LoginUser>> DeleteLoginUser(int id)
      {
         var loginUser = await _context.LoginUserSet.FindAsync(id);
         if (loginUser == null)
         {
            return NotFound();
         }

         _context.LoginUserSet.Remove(loginUser);
         await _context.SaveChangesAsync();

         return loginUser;
      }

      private bool LoginUserExists(int id)
      {
         return _context.LoginUserSet.Any(e => e.Id == id);
      }
   }
}
