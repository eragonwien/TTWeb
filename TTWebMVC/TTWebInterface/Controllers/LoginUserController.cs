using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTWebCommon.Models;

namespace TTWebInterface.Controllers
{
   public class LoginUserController : BaseController
   {
      private readonly TTWebDbContext db;

      public LoginUserController(TTWebDbContext db)
      {
         this.db = db;
      }

      // GET: LoginUser
      public async Task<IActionResult> Index()
      {
         return View(await db.LoginUserSet.ToListAsync());
      }

      // GET: LoginUser/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var loginUser = await db.LoginUserSet
             .FirstOrDefaultAsync(m => m.Id == id);
         if (loginUser == null)
         {
            return NotFound();
         }

         return View(loginUser);
      }

      // GET: LoginUser/Create
      public IActionResult Create()
      {
         return View();
      }

      // POST: LoginUser/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,Email,Password,Title,Firstname,Lastname")] LoginUser loginUser)
      {
         if (ModelState.IsValid)
         {
            db.Add(loginUser);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         return View(loginUser);
      }

      // GET: LoginUser/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var loginUser = await db.LoginUserSet.FindAsync(id);
         if (loginUser == null)
         {
            return NotFound();
         }
         return View(loginUser);
      }

      // POST: LoginUser/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password,Title,Firstname,Lastname")] LoginUser loginUser)
      {
         if (id != loginUser.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               db.Update(loginUser);
               await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!LoginUserExists(loginUser.Id))
               {
                  return NotFound();
               }
               else
               {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         return View(loginUser);
      }

      // GET: LoginUser/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var loginUser = await db.LoginUserSet
             .FirstOrDefaultAsync(m => m.Id == id);
         if (loginUser == null)
         {
            return NotFound();
         }

         return View(loginUser);
      }

      // POST: LoginUser/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var loginUser = await db.LoginUserSet.FindAsync(id);
         db.LoginUserSet.Remove(loginUser);
         await db.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool LoginUserExists(int id)
      {
         return db.LoginUserSet.Any(e => e.Id == id);
      }
   }
}
