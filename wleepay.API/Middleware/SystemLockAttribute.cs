using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using wleepay.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using wleepay.Services;


namespace wleepay.API.Middleware
{
    public class SystemLockAttribute : TypeFilterAttribute
    {

        public SystemLockAttribute() : base(typeof(SystemLockFilter))
        {
        }

        private class SystemLockFilter : IAsyncActionFilter
        {
            private readonly ISystemStatusService _statusService;

            public SystemLockFilter(ISystemStatusService statusService)
            {
                _statusService = statusService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var systemStatus = await _statusService.GetSystemStatus(); // Retrieve system status from service

                if (systemStatus.Status)
                {
                    context.Result = new ContentResult
                    {
                        StatusCode = StatusCodes.Status503ServiceUnavailable,
                        Content = "System is currently not available"
                    };
                    return;
                }

                await next();
            }

            //private async Task<bool> CheckSystemLockStatus()
            //{
            //    var result = await _systemStatusService.GetSystemStatus();
            //    return result.Status;
            //}
        }
    }

}

