using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using wleepay.API.Helpers;
namespace wleepay.API.Middleware
{
    public class OtpExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CustomException otpException)
            {
                context.Result = new BadRequestObjectResult(new { message = otpException.Message });
                context.ExceptionHandled = true;
            }
        }
    }
}
