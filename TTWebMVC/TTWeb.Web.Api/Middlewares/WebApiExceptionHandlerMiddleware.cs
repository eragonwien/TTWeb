using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TTWeb.BusinessLogic.Exceptions;

namespace TTWeb.Web.Api.Middlewares
{
    public class WebApiExceptionHandlerMiddleware
    {
        private readonly ILogger<WebApiExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public WebApiExceptionHandlerMiddleware(RequestDelegate next, 
            ILogger<WebApiExceptionHandlerMiddleware> logger, 
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HandleInnerException();
            var statusCode = HttpStatusCode.InternalServerError;
            var message = ex.Message;
            var stackTrace = _env.IsDevelopment() ? ex.StackTrace : null;

            switch (ex)
            {
                case InvalidInputException invalidInput:
                case InvalidTokenException invalidToken:
                case ResourceNotFoundException resourceNotFound:
                case ResourceAccessDeniedException resourceAccessDenied:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case SqlException sqlException:
                    HandleSqlException(sqlException);
                    break;
            }


            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) statusCode;
            await httpContext.Response.WriteAsync(new
            {
                httpContext.Response.StatusCode,
                message,
                stackTrace
            }.ToString());

            void HandleInnerException()
            {
                if (ex?.InnerException == null) return;

                if (ex.InnerException is SqlException)
                    ex = ex.InnerException;
            }

            void HandleSqlException(SqlException sqlException)
            {
                switch (sqlException.Number)
                {
                    case 2601:
                        statusCode = HttpStatusCode.BadRequest;
                        message = "Duplicate insert error";
                        stackTrace = null;
                        break;
                }
            }
        }
    }
}