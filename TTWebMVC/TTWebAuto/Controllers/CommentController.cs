using Microsoft.AspNetCore.Mvc;
using System;
using TTWebAuto.Models;

namespace TTWebAuto.Controllers
{
   public class CommentController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Index(Comment comment)
      {
         try
         {
            comment.Execute();
            return RedirectToAction();
         }
         catch (Exception ex)
         {
            return View(comment);
         }
      }
   }
}