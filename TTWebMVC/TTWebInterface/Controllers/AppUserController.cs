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
   public class AppUserController : BaseController
   {
      private readonly TTWebDbContext db;
      public AppUserController(TTWebDbContext context)
      {
         this.db = context;
      }

      // GET: AppUser
      public async Task<IActionResult> Index()
      {
         return View(await db.AppUserSet.ToListAsync());
      }

      // GET: AppUser/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var appUser = await db.AppUserSet
             .FirstOrDefaultAsync(m => m.Id == id);
         if (appUser == null)
         {
            return NotFound();
         }

         return View(appUser);
      }

      // GET: AppUser/Create
      public IActionResult Create()
      {
         return View();
      }

      // POST: AppUser/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,Email,Password,Firstname,Lastname")] AppUser appUser)
      {
         if (ModelState.IsValid)
         {
            db.Add(appUser);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         return View(appUser);
      }

      // GET: AppUser/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var appUser = await db.AppUserSet.FindAsync(id);
         if (appUser == null)
         {
            return NotFound();
         }
         return View(appUser);
      }

      // POST: AppUser/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password,Firstname,Lastname")] AppUser appUser)
      {
         if (id != appUser.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               db.Update(appUser);
               await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!AppUserExists(appUser.Id))
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
         return View(appUser);
      }

      // GET: AppUser/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var appUser = await db.AppUserSet
             .FirstOrDefaultAsync(m => m.Id == id);
         if (appUser == null)
         {
            return NotFound();
         }

         return View(appUser);
      }

      // POST: AppUser/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var appUser = await db.AppUserSet.FindAsync(id);
         db.AppUserSet.Remove(appUser);
         await db.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool AppUserExists(int id)
      {
         return db.AppUserSet.Any(e => e.Id == id);
      }
   }
}
