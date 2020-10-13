using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTWeb.Data.Database;
using TTWeb.Data.Models;

namespace TTWeb.Web.Controllers
{
    public class LoginUserController : TTWebBaseController
    {
        private readonly TTWebContext _context;

        public LoginUserController(TTWebContext context)
        {
            _context = context;
        }

        // GET: LoginUser
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoginUsers.ToListAsync());
        }

        // GET: LoginUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginUser = await _context.LoginUsers
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,FirstName,LastName")] LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginUser);
                await _context.SaveChangesAsync();
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

            var loginUser = await _context.LoginUsers.FindAsync(id);
            if (loginUser == null)
            {
                return NotFound();
            }
            return View(loginUser);
        }

        // POST: LoginUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,FirstName,LastName")] LoginUser loginUser)
        {
            if (id != loginUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginUser);
                    await _context.SaveChangesAsync();
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

            var loginUser = await _context.LoginUsers
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
            var loginUser = await _context.LoginUsers.FindAsync(id);
            _context.LoginUsers.Remove(loginUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginUserExists(int id)
        {
            return _context.LoginUsers.Any(e => e.Id == id);
        }
    }
}
