using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTWebCommon.Models.Common;
using TTWebCommon.Models.Common.Exceptions;

namespace TTWebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ErrorDetailResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // Your exception
            var errorResponse = new ErrorDetailResponse();

            if (exception is WebApiNotFoundException) 
                errorResponse.StatusCode = 404; // Not Found
            else if (exception is WebApiUnauthorizedException)
                errorResponse.StatusCode = 401; // Unauthorized
            else if (exception is WebApiForbiddenException)
                errorResponse.StatusCode = 403; // forbidden
            else if (exception is WebApiBadRequestException)
                errorResponse.StatusCode = 400; // Bad Request
            else
            {
                errorResponse.StatusCode = 500;
                errorResponse.Title = exception?.Message;
                errorResponse.StackTrace = exception?.StackTrace;
            }

            return errorResponse;
        }
    }
}
