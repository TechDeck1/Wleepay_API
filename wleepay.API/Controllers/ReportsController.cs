using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using wleepay.Core.Services;
using wleepay.Services;
using wleepay.API.Resources;
using System.Linq;
using Microsoft.VisualBasic;
using System;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using wleepay.Core.Models;
using AutoMapper;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [AllowAnonymous]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IUserDetailService _userDetailService;
        private readonly IMapper _mapper;


        public ReportsController(IReportService reportService, IUserDetailService userDetailService, IMapper mapper)
        {
            _reportService = reportService;
            _userDetailService = userDetailService;
            _mapper = mapper;
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
            var data = await _reportService.GetSubscriberAndMemberListByWalletBalanceAsync(reportByWalletBalanceRequest.CurrencyType, reportByWalletBalanceRequest.Role , reportByWalletBalanceRequest.Balance , reportByWalletBalanceRequest.WalletType);
            return Ok(data);
        }

        [HttpGet("ReportBySubscribersKYCLevel")]
        public async Task<IActionResult> GetReportBySubscribersKYCLevelAsync([FromQuery]int role, [FromQuery]int level)
        {
            var data = await _reportService.GetReportBySubscribersKYCLevelAsync(level, role);
            return Ok(data);
        }

        [HttpGet("AllActiveAccounts")]
        public async Task<IActionResult> GetReportByAllActiveAccountsAsync([FromQuery] int role)
        {
            var data = await _reportService.GetAllActiveAccountsAsync(role);
            return Ok(data);
        }


        [HttpGet("AccountOpenedByDateRange")]
        public async Task<IActionResult> GetAccountOpenedByDateRangeAsync([FromQuery] DateTime fromDate , DateTime toDate)
        {
            var data = await _reportService.GetAccountOpenedByDateRangeAsync(fromDate, toDate);
            return Ok(data);
        }

        [HttpGet("SubscribersCountByCountry")]
        public async Task<IActionResult> GetSubscribersCountByCountryAsync([FromQuery] int countryId)
        {
            var data = await _reportService.GetSubscribersCountByCountryAsync(countryId);
            return Ok(data);
        }

        [HttpPost("TransactionCountSummary")]
        public async Task<IActionResult> GetTransactionCountSummaryAsync(TransactionCountSummaryRequest transactionCountSummaryRequest)
        {
            var data = await _reportService.TransactionCountSummary(transactionCountSummaryRequest.Role , transactionCountSummaryRequest.TransferType, transactionCountSummaryRequest.fromDate, transactionCountSummaryRequest.toDate);
            var transactionResources = data.Transactions.Select(transaction => _mapper.Map<Transaction, TransactionHistoryResource>(transaction)).ToList();

            return Ok(new
            {
                TotalCount = data.TotalCount,
                Transactions = transactionResources
            });
        }


    }
}
