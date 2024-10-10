using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wleepay.Core.Services;
using Microsoft.AspNetCore.Authorization;
using wleepay.Services;


namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [AllowAnonymous]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            var data = await _dashboardService.GetDashboardDataAsync();
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("SubscribersAndAgentsOnBoardByMonth")]
        public async Task<IActionResult> activeAgentsSubscribersByMonth()
        {
            var agentMemberCount = await _dashboardService.GetActiveAgentsAndSubscribersCount();
            var activeAgentsByMonth = await _dashboardService.GetActiveAgentsByMonth();
            var activeSubscribersByMonth = await _dashboardService.GetActiveSubscribersByMonth();
            var activeSubscribersByType = await _dashboardService.GetActiveSubscribersByType();
            return Ok(new
            {
                agentMemberCount,
                activeAgentsByMonth,
                activeSubscribersByMonth,
                activeSubscribersByType
            });
        }

        [AllowAnonymous]
        [HttpGet("TransactionAmountByMonth")]
        public async Task<IActionResult> GetTransactionAmountByMonth([FromQuery] int? monthId, int currencyId)
        {
            var transactionRequestLD = await _dashboardService.GetTransactionAmountByMonthLD();
            var transactionRequestUSD = await _dashboardService.GetTransactionAmountByMonthUSD();
            var transactionData = await _dashboardService.GetTransactionData((int)monthId, (int)currencyId);
            return Ok(new { transactionRequestLD, transactionRequestUSD, transactionData });

        }

        [AllowAnonymous]
        [HttpGet("TransactionCountByType")]
        public async Task<IActionResult> GetTransactionCountByTypeLD()
        {
            var transactionRequestLD = await _dashboardService.GetTransactionCounttByTypeLD();
            var transactionRequestUSD = await _dashboardService.GetTransactionCounttByTypeUSD();

            return Ok(new { transactionRequestLD, transactionRequestUSD });

        }

        [AllowAnonymous]
        [HttpGet("WalletAmountHistoryByMonth")]
        public async Task<IActionResult> GetWalletHistoryByWalletTypeAndCurrency([FromQuery] int walletType, int currencyId)
        {

            var walletData = await _dashboardService.GetWalletHistoryByWalletTypeAndCurrency(walletType, currencyId);
            return Ok(walletData);
        }

    }
}
