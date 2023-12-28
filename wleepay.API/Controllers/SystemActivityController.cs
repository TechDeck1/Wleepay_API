using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using wleepay.API.Resources;

namespace wleepay.API.Controllers
{
    public class SystemActivityController : Controller
    {

        private readonly ISystemActivityLogsService _systemActivityLogsService;

        public SystemActivityController(
            ISystemActivityLogsService systemActivityLogsService
            )
        {
           
            _systemActivityLogsService = systemActivityLogsService;
        }

        [HttpPost("SystemLogs")]
        public async Task<IActionResult> GetSystemAllLogs([FromBody] Pagination request)
        {
            var logs =await _systemActivityLogsService.GetAllSystemLogs(request.Index, request.Size);
            return Ok(logs);
        }

        [HttpGet("SystemLogsById/{userId}")]
        public async Task<IActionResult> GetSystemLogsById(string userId)
        {
            var logs = await _systemActivityLogsService.GetAllSystemById(userId);
            return Ok(logs);
        }
    }
}
