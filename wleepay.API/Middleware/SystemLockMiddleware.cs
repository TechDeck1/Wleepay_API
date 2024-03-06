using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using wleepay.Core.Services;

namespace wleepay.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SystemLockMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISystemStatusService _systemStatusService;


        public SystemLockMiddleware(RequestDelegate next, ISystemStatusService statusService)
        {
            _next = next;
            _systemStatusService = statusService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method == HttpMethods.Post || httpContext.Request.Method == HttpMethods.Put &&
            IsLoginOrRefreshTokenRequest(httpContext.Request.Path))
            {
                // Check system lock status (you need to implement this logic)
                bool systemLocked = await CheckSystemLockStatus();

                if (systemLocked)
                {
                    // If system is locked, return a 403 Forbidden response
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await httpContext.Response.WriteAsync("System is locked. POST and PUT requests are disabled.");
                    return;
                }
            }

            await _next(httpContext);
        }

        private async Task<bool> CheckSystemLockStatus()
        {
            var result = await _systemStatusService.GetSystemStatus();
            return result.Status;
        }

        private bool IsLoginOrRefreshTokenRequest(PathString path)
        {
            // Check if the request path matches the login or refresh token endpoint
            // Adjust this method according to your application's route structure
            return path.StartsWithSegments("/login") || path.StartsWithSegments("/refresh-token");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SystemLockMiddlewareExtensions
    {
        public static IApplicationBuilder UseSystemLockMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SystemLockMiddleware>();
        }
    }
}
