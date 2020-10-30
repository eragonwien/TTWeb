using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TTWebCommon.Middlewares
{
   public class ExceptionLoggingMiddleware
   {
      private readonly RequestDelegate _next;
      private readonly ILogger<ExceptionLoggingMiddleware> log;

      public ExceptionLoggingMiddleware(RequestDelegate next, ILogger<ExceptionLoggingMiddleware> log)
      {
         _next = next;
         this.log = log;
      }

      public async Task Invoke(HttpContext httpContext)
      {
         try
         {
            await _next(httpContext);
         }
         catch (Exception ex)
         {
            log.LogError("{0}: {1}", ex.Message, ex.StackTrace);
            throw;
         }
      }
   }

   public static class ExceptionLoggingMiddlewareExtension
   {
      public static IApplicationBuilder UseExceptionLogging(this IApplicationBuilder builder)
      {
         return builder.UseMiddleware<ExceptionLoggingMiddleware>();
      }
   }
}
