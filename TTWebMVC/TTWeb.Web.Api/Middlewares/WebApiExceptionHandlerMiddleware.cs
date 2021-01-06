using System;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models.Helpers;

namespace TTWeb.Web.Api.Middlewares
{
    public class WebApiExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public WebApiExceptionHandlerMiddleware(RequestDelegate next,
            IHostEnvironment env)
        {
            _next = next;
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
                case IBadRequestException _:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException _:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case SqlException sqlException:
                    HandleSqlException(sqlException);
                    break;
            }

            await WriteResponseAsync();

            async Task WriteResponseAsync()
            {
                httpContext.Response.ContentType = "application/json; charset=utf-8";
                httpContext.Response.StatusCode = (int)statusCode;
                await httpContext.Response.WriteAsync(new ErrorResponseModel(statusCode, message, stackTrace).ToJsonString());
            }

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
                    case 547:
                        message = "Invalid reference Id of input";
                        stackTrace = null;
                        break;
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