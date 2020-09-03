using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Mozilla;
using TTWebCommon.Models.Common;
using TTWebCommon.Models.Common.Exceptions;
using TTWebCommon.Services;

namespace TTWebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly IExceptionService exceptionService;

        public ErrorController(IExceptionService exceptionService)
        {
            this.exceptionService = exceptionService;
        }

        [Route("error")]
        public ErrorDetailResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // Your exception
            var errorResponse = new ErrorDetailResponse();

            if (exception is MySqlException)
                exceptionService.HandleMySqlException(exception as MySqlException);

            if (exception is WebApiNotFoundException) 
                errorResponse.StatusCode = 404; // Not Found
            else if (exception is WebApiUnauthorizedException)
                errorResponse.StatusCode = 401; // Unauthorized
            else if (exception is WebApiForbiddenException)
                errorResponse.StatusCode = 403; // forbidden
            else if (exception is WebApiBadRequestException || exception is WebApiDuplicateInsertException)
                errorResponse.StatusCode = 400; // Bad Request
            else
                errorResponse.StatusCode = 500;

            errorResponse.Title = exception?.Message;

            if (errorResponse.StatusCode == 500)
                errorResponse.StackTrace = exception?.StackTrace;

            Response.StatusCode = errorResponse.StatusCode;
            return errorResponse;
        }
    }
}
