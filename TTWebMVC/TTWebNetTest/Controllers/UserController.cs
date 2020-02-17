using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TTWebNetCommon.Models;

namespace TTWebNetTest.Controllers
{
   public class UserController : BaseController
   {
      // GET: User
      public async Task<ActionResult> Index()
      {
         return View(await db.AppUserSet.AsNoTracking().ToListAsync());
      }

      // GET: User/Details/5
      public ActionResult Details(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         AppUser appUser = db.AppUserSet.Find(id);
         if (appUser == null)
         {
            return HttpNotFound();
         }
         return View(appUser);
      }

      // GET: User/Create
      public ActionResult Create()
      {
         return View();
      }

      // POST: User/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "Id,Email,Password,Firstname,Lastname")] AppUser appUser)
      {
         if (ModelState.IsValid)
         {
            db.AppUserSet.Add(appUser);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         return View(appUser);
      }

      // GET: User/Edit/5
      public ActionResult Edit(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         AppUser appUser = db.AppUserSet.Find(id);
         if (appUser == null)
         {
            return HttpNotFound();
         }
         return View(appUser);
      }

      // POST: User/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "Id,Email,Password,Firstname,Lastname")] AppUser appUser)
      {
         if (ModelState.IsValid)
         {
            db.Entry(appUser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(appUser);
      }

      // GET: User/Delete/5
      public ActionResult Delete(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         AppUser appUser = db.AppUserSet.Find(id);
         if (appUser == null)
         {
            return HttpNotFound();
         }
         return View(appUser);
      }

      // POST: User/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         AppUser appUser = db.AppUserSet.Find(id);
         db.AppUserSet.Remove(appUser);
         db.SaveChanges();
         return RedirectToAction("Index");
      }
   }
}
