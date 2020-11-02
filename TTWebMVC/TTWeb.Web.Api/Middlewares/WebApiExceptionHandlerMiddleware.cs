using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TTWeb.BusinessLogic.Exceptions;

namespace TTWeb.Web.Api.Middlewares
{
    public class WebApiExceptionHandlerMiddleware
    {
        private readonly ILogger<WebApiExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public WebApiExceptionHandlerMiddleware(RequestDelegate next, ILogger<WebApiExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var errorMessage = ex.Message;

            switch (ex)
            {
                case InvalidInputException invalidInput:
                case InvalidTokenException invalidToken:
                case ResourceNotFoundException resourceNotFound:
                case InsertOperationFailedException insertOperationFailed:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
            }


            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) statusCode;
            return httpContext.Response.WriteAsync(new
            {
                httpContext.Response.StatusCode,
                Message = errorMessage
            }.ToString());
        }
    }
}