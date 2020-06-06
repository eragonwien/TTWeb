using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TTWebApi.Middlewares
{
   public class OverrideContextUserMiddleware
   {
      private readonly RequestDelegate next;
      public OverrideContextUserMiddleware(RequestDelegate next)
      {
         this.next = next;
      }

      public async Task Invoke(HttpContext context)
      {

      }
   }
}
