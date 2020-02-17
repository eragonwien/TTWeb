using System.Web.Mvc;
using TTWebNetCommon.Facebook;
using TTWebNetCommon.Models;

namespace TTWebNetTest.Controllers
{
   public class BaseController : Controller
   {
      protected readonly TTWebDbContext db = new TTWebDbContext();
      protected readonly IFacebookService fb = new FacebookService();

      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            db.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}