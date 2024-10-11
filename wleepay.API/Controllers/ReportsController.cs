using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using wleepay.Core.Services;
using wleepay.Services;
using wleepay.API.Resources;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [AllowAnonymous]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("RiskReview")]
        public async Task<IActionResult> GetReportOfUserByStatusAsync(ReportByUserStatusRequest reportByUserStatus)
        {
            var data = await _reportService.GetReportOfUserByStatusAsync(reportByUserStatus.Role, reportByUserStatus.Status, reportByUserStatus.PageIndex=0, reportByUserStatus.PageSize=10);
            return Ok(data);
        }
    }
}
