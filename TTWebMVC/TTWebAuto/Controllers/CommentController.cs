using Microsoft.AspNetCore.Mvc;
using System;
using TTWebAuto.Models;

namespace TTWebAuto.Controllers
{
   public class CommentController : DefaultController
   {
      public IActionResult Index()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Comment(FacebookModel model)
      {
         model.Comment();
         return RedirectToAction();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Friends(FacebookModel model)
      {
         var friends = model.FriendList();
         return View(friends);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult SearchFriend(FacebookModel model)
      {
         var friends = model.SearchFriend();
         return View("Friends", friends);
      }
   }
}