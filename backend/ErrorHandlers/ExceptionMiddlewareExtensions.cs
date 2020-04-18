using Microsoft.AspNetCore.Builder;

namespace backend.ErrorHandlers
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandlers(this IApplicationBuilder app)
        {
            app.UseMiddleware<UserFriendlyExceptionMiddleware>();
        }
    }
}