using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wleepay.Core.Services;
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

        [HttpGet("SystemLogs")]
        public async Task<IActionResult> GetSystemAllLogs(Pagination request)
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

        [HttpGet("SystemLogsById")]
        public async Task<IActionResult> GetSystemLogsById(SystemLogsPagination request)
        {
            var logs = await _systemActivityLogsService.GetAllSystemById(request.UserId, request.Index , request.Size);
            return Ok(logs);
        }

        [HttpGet("FilterLogsByEmail")]
        public async Task<IActionResult> FilterLogsByEmail(FilterLogs filterLogs)
        {
            var logs = await _systemActivityLogsService.FilterLogsByEmail(filterLogs.Email , filterLogs.Index, filterLogs.Size);
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
