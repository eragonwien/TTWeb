using System;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json.Linq;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.Data.Models;
using TTWeb.Web.Api.Extensions;

namespace TTWeb.Web.Api.Components.Attributes
{
    public class AccessOwnResourceFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;
            await ThrowExceptionOnLoginUserIdMismatch();


            async Task ThrowExceptionOnLoginUserIdMismatch()
            {
                if (!user.Identity.IsAuthenticated) return;
                if (user.IsInRole(UserPermission.AccessAllResources)) return;

                if (!user.IsInRole(UserPermission.AccessOwnResources))
                    throw new ResourceAccessDeniedException();

                if (!await AreLoginUserIdsMismatch())
                    throw new ResourceAccessDeniedException();
            }

            async Task<bool> AreLoginUserIdsMismatch()
            {
                var request = context.HttpContext.Request;
                request.EnableBuffering();
                var len = request.ContentLength;
                string body;
                using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                    body = await reader.ReadToEndAsync();

                request.Body.Seek(0, SeekOrigin.Begin);

                if (string.IsNullOrWhiteSpace(body)) return true;
                if (!TryParse(body, out var jsonBody)) return true;

                var ownerId = (int?)jsonBody.GetValue("ownerId");

                if (!ownerId.HasValue) return true;

                var loginUserId = user.FindFirstValue<int>(ClaimTypes.NameIdentifier);
                return ownerId == loginUserId;
            }

            await base.OnActionExecutionAsync(context, next);

            bool TryParse(string str, out JObject parsedJObject)
            {
                try
                {
                    parsedJObject = JObject.Parse(str);
                    return true;
                }
                catch (Exception)
                {
                    parsedJObject = null;
                    return false;
                }
            }
        }
    }
}