using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wleepay.Core.Services;
using wleepay.API.Resources;
using Microsoft.AspNetCore.Authorization;
using wleepay.API.Middleware;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    //[AllowAnonymous]
    public class SystemActivityController : Controller
    {

        private readonly ISystemActivityLogsService _systemActivityLogsService;

        public SystemActivityController(
            ISystemActivityLogsService systemActivityLogsService)
        {
           
            _systemActivityLogsService = systemActivityLogsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSystemAllLogs([FromQuery]Pagination request)
        {
            if (!string.IsNullOrEmpty(request.Email))
            {
                var logs = await _systemActivityLogsService.FilterLogsByEmail(request.Email, request.Index, request.Size);
                return Ok(logs);
            }
            else
            {
                var logs = await _systemActivityLogsService.GetAllSystemLogs(request.Index, request.Size);
                return Ok(logs);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetSystemLogsById(string userId, [FromQuery] int pageIndex=0, [FromQuery] int pageSize=10)
        {
            var logs = await _systemActivityLogsService.GetAllSystemById(userId, pageIndex, pageSize);
            return Ok(logs);
        }

        //[HttpGet("{email}")]
        //public async Task<IActionResult> FilterLogsByEmail(string email, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        //{
        //    var logs = await _systemActivityLogsService.FilterLogsByEmail(email, pageIndex, pageSize);
        //    return Ok(logs);
        //}

        [SystemLock]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogsById(int id)
        {
            var result = await _systemActivityLogsService.DeleteLogsById(id);
            return Ok(result);
        }
    }
}
