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

        [HttpGet("InActiveUsers")]
        public async Task<IActionResult> GetInActiveSubscriberListAsync([FromQuery]int role)
        {
            var data = await _reportService.GetInActiveSubscriberListAsync(role);
            return Ok(data);
        }
        [HttpGet("ActiveUsers")]
        public async Task<IActionResult> GetActiveSubscriberListAsync([FromQuery] int role)
        {
            var data = await _reportService.GetActiveSubscriberListAsync(role);
            return Ok(data);
        }

        [HttpPost("ReportByWalletBalance")]
        public async Task<IActionResult> GetSubscriberAndMemberListByWalletBalanceAsync(ReportByWalletBalanceRequest reportByWalletBalanceRequest)
        {
            var data = await _reportService.GetSubscriberAndMemberListByWalletBalanceAsync(reportByWalletBalanceRequest.WalletType, reportByWalletBalanceRequest.Role , reportByWalletBalanceRequest.Balance);
            return Ok(data);
        }

    }
}
