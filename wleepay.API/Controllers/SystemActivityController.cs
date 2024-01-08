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
            ISystemActivityLogsService systemActivityLogsService)
        {
           
            _systemActivityLogsService = systemActivityLogsService;
        }

        [HttpPost("SystemLogs")]
        public async Task<IActionResult> GetSystemAllLogs(Pagination request)
        {
            var logs =await _systemActivityLogsService.GetAllSystemLogs(request.Index, request.Size);
            return Ok(logs);
        }

        [HttpGet("SystemLogsById")]
        public async Task<IActionResult> GetSystemLogsById(SystemLogsPagination request)
        {
            var logs = await _systemActivityLogsService.GetAllSystemById(request.UserId, request.Index , request.Size);
            return Ok(logs);
        }

        [HttpGet("FilterLogsByEmail")]
        public async Task<IActionResult> FilterLogsByEmail(FilterLogs filterLogs)
        {
            var logs = await _systemActivityLogsService.FilterLogsByEmail(filterLogs.email , filterLogs.Index, filterLogs.Size);
            return Ok(logs);
        }

        [HttpDelete("DeleteLogsById/{id}")]
        public async Task<IActionResult> DeleteLogsById(int id)
        {
            var result = await _systemActivityLogsService.DeleteLogsById(id);
            return Ok(result);
        }
    }
}
