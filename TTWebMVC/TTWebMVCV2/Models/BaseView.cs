using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVCV2.Models
{
   public abstract class BaseView<TModel> : RazorPage<TModel>
   {
      public bool Authenticated
      {
         get
         {
            return User.Identity.IsAuthenticated;
         }
      }
   }
}
