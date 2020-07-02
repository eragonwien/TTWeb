using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTWebApi.Services;
using TTWebCommon.Models;

namespace TTWebMVCV2.Models
{
   public abstract class BaseView<TModel> : RazorPage<TModel>
   {
      [RazorInject]
      public IAppUserService AppUserService { get; set; }
      public bool Authenticated
      {
         get
         {
            return User.Identity.IsAuthenticated;
         }
      }

      public AppUser ContextUser
      {
         get
         {
            return AppUserService.LoadContextUser(Context.User);
         }
      }
   }
}
