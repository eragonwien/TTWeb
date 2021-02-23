using System;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Npgsql;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.BusinessLogic.Models;

namespace TTWeb.Web.Api.Middlewares
{
    public class WebApiExceptionHandlerMiddleware
    {
        private readonly IHostEnvironment _env;
        private readonly RequestDelegate _next;

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
            var detailedMessage = ex.Message;
            var stackTrace = ex.StackTrace;

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
                case PostgresException postgresException:
                    HandlePostgresSqlException(postgresException);
                    break;
            }

            message = _env.IsDevelopment() ? detailedMessage : message;
            stackTrace = _env.IsDevelopment() ? stackTrace : null;

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

                if (ex.InnerException is SqlException ||
                    ex.InnerException is PostgresException)
                    ex = ex.InnerException;
            }

            void HandleSqlException(SqlException sqlException)
            {
                switch (sqlException.Number)
                {
                    case 547:
                        message = "Invalid reference Id of input";
                        break;
                    case 2601:
                        statusCode = HttpStatusCode.BadRequest;
                        message = "Duplicate insert error";
                        break;
                }

                stackTrace = sqlException.StackTrace;
                detailedMessage = sqlException.Message;
            }

            void HandlePostgresSqlException(PostgresException postgresException)
            {
                switch (postgresException.SqlState)
                {
                    case PostgresErrorCodes.ForeignKeyViolation:
                        statusCode = HttpStatusCode.BadRequest;
                        message = "Request contains invalid reference";
                        break;
                    case PostgresErrorCodes.UniqueViolation:
                        statusCode = HttpStatusCode.BadRequest;
                        message = "This entry is already exists";
                        break;
                    default:
                        statusCode = HttpStatusCode.InternalServerError;
                        message = $"Unhandled Postgres SQL exception: {postgresException.Message}";
                        break;
                }

                stackTrace = postgresException.StackTrace;
                detailedMessage = postgresException.Message;
            }
        }
    }
}