using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TTWebApi.Models;

namespace TTWebApi.Controllers
{
   [ApiController]
   public class BaseController : ControllerBase
   {
      public ContextUser ContextUser
      {
         get
         {
            return new ContextUser(User);
         }
      }

   }
}
