using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using wleepay.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using wleepay.Services;
using System.Linq;
using static System.Net.WebRequestMethods;
using wleepay.Core.Enums;
using System;

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
                var userRoles = context.HttpContext.User.Claims
               .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
               .Select(c => c.Value)
               .ToList();

                string userRole="";
                foreach (var role in userRoles)
                {
                    userRole= role.ToString();
                }
                if ( userRole == Role.Staff.ToString() || userRole == Role.Agent.ToString() || userRole == Role.Member.ToString())
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

