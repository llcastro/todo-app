using System;
using System.Net;
using System.Threading.Tasks;
using backend.CustomExceptions;
using Microsoft.AspNetCore.Http;

namespace backend.ErrorHandlers
{
    public class UserFriendlyExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public UserFriendlyExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (UserFriendlyException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, new UserFriendlyException(ex.Message));
            }
        }

        private Task HandleExceptionAsync(HttpContext context, UserFriendlyException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode.GetHashCode();

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = exception.StatusCode.GetHashCode(),
                Message = exception.Message
            }.ToString());
        }
    }
}