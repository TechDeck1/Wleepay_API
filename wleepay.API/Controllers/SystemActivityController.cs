using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

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

        [HttpGet("SystemLogs")]
        public async Task<IActionResult> GetSystemAllLogs()
        {
            var logs =await _systemActivityLogsService.GetAllSystemLogs();
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
