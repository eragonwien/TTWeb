using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Mozilla;
using TTWebCommon.Models;
using TTWebCommon.Models.Common;
using TTWebCommon.Models.Common.Exceptions;
using TTWebCommon.Services;

namespace TTWebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly IExceptionService exceptionService;
        private readonly ILogger<ErrorController> log;

        public ErrorController(IExceptionService exceptionService, ILogger<ErrorController> log)
        {
            this.exceptionService = exceptionService;
            this.log = log;
        }

        [Route("error")]
        public ErrorDetailResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // Your exception
            var errorResponse = new ErrorDetailResponse();
            var errorCode = ErrorCode.INTERNAL_ERROR;

            if (exception is MySqlException)
                exception = exceptionService.HandleMySqlException(exception as MySqlException);

            if (exception is WebApiNotFoundException) 
                errorResponse.StatusCode = 404; // Not Found
            else if (exception is WebApiUnauthorizedException)
                errorResponse.StatusCode = 401; // Unauthorized
            else if (exception is WebApiForbiddenException)
                errorResponse.StatusCode = 403; // forbidden
            else if (exception is WebApiBadRequestException)
                errorResponse.StatusCode = 400; // Bad Request
            else if (exception is WebApiDuplicateInsertException)
            {
                errorResponse.StatusCode = 400;
                errorCode = ErrorCode.DUPLICATE_INSERT;
            }
            else
                errorResponse.StatusCode = 500;

            errorResponse.Title = exception?.Message;
            errorResponse.ErrorCodeValue = errorCode;

            if (errorResponse.StatusCode == 500)
            {
                log.LogError(exception?.Message);
                log.LogError(exception?.StackTrace);
            }

            Response.StatusCode = errorResponse.StatusCode;
            return errorResponse;
        }
    }
}
