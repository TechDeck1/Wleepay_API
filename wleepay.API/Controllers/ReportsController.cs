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
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using static DevExpress.XtraPrinting.Native.PageSizeInfo;

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

        [HttpPost("ListOfUserWithminimum5Tickets")]
        public async Task<IActionResult> GetReportOfUserByOpenTicketsAsync(ReportByUserStatusRequest reportByUserStatus)
        {
            var data = await _reportService.GetReportOfUserByOpenTicketsAsync(reportByUserStatus.Role, reportByUserStatus.Status, reportByUserStatus.PageIndex = 0, reportByUserStatus.PageSize = 10);
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

        [HttpGet("SubscribersCountByAgeRange")]
        public async Task<IActionResult> GetSubscribersCountByAgeAsync([FromQuery] int minAge,int maxAge)
        {
            var data = await _reportService.GetSubscribersCountByAgeAsync(minAge,maxAge);
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

        [HttpPost("LecAirtimeCommssionEarnedReport")]
        public async Task<IActionResult> LecAirtimeCommssionEarnedReport(SochitelTransactionCommisionSummaryRequest transactionCountSummaryRequest)
        {
            var data = await _reportService.SochitelCommisionTransactionSummary(transactionCountSummaryRequest.Role, transactionCountSummaryRequest.TransactionType, transactionCountSummaryRequest.fromDate, transactionCountSummaryRequest.toDate);
            var transactionResources = data.Transactions.Select(transaction => _mapper.Map<SochitelTransactions, SochitelTransactionCommissionEarnedResource>(transaction)).ToList();

            return Ok(new
            {
                TotalCount = data.TotalCount,
                Transactions = transactionResources,
                CommissionRate = data.CommisionEarned
            });
        }

        [HttpPost("GetStaringBalance")]
        public async Task<IActionResult> GetStaringWalletBalance(GetWalletBalanceResponse getWalletBalanceResponse)
        {
            var data = await _reportService.getStartingWalletBalance(getWalletBalanceResponse.DateTime, getWalletBalanceResponse.WalletType, getWalletBalanceResponse.CurrencyId);

            return Ok(data);
        }

        [HttpPost("GetEndingBalance")]
        public async Task<IActionResult> GetEndingWalletBalance(GetWalletBalanceResponse getWalletBalanceResponse)
        {
            var data = await _reportService.getEndingWalletBalance(getWalletBalanceResponse.DateTime, getWalletBalanceResponse.WalletType, getWalletBalanceResponse.CurrencyId);

            return Ok(data);
        }

        [HttpPost("GenerateReportWeeklyBalance")]
        public async Task<IActionResult> GetReportWeeklyBalance(GetReportWeeklyBalanceResponse getWalletBalanceResponse)
        {
            var data = await _reportService.getWalletBalance(getWalletBalanceResponse.ReportStartingBalance, getWalletBalanceResponse.ReportEndingBalance, getWalletBalanceResponse.WalletType, getWalletBalanceResponse.CurrencyId);

            return Ok(data);
        }

        [HttpPost("Credits")]
        public async Task<IActionResult> getCreditsForAgentAndMemberByWleepay()
        {
            var data = await _reportService.getCreditsForAgentAndMemberByWleepay();

            return Ok(data);
        }

        [HttpPost("Debits")]
        public async Task<IActionResult> getDebitsForAgentAndMemberByWleepay()
        {
            var data = await _reportService.getDebitsForAgentAndMemberByWleepay();

            return Ok(data);
        }


        [HttpPost("GetCashOnHandBalance")]
        public IActionResult GetCashOnHandBalance(GetCashOnHandByWleepay getWalletBalanceResponse)
        {
            var data = _reportService.getCashOnHandByWleepay(getWalletBalanceResponse.FromDate, getWalletBalanceResponse.ToDate, getWalletBalanceResponse.TransferType, getWalletBalanceResponse.CurrencyType, getWalletBalanceResponse.PageSize, getWalletBalanceResponse.PageIndex);

            return Ok(data);
        }

        [HttpPost("GetBillPaymentCommissionEarned")]
        public IActionResult GetBillPaymentCommissionEarnedReport(GetBusinessReport getWalletBalanceResponse)
        {
            var data = _reportService.getBillPaymentCommissionEarnedReport(getWalletBalanceResponse.FromDate, getWalletBalanceResponse.ToDate, getWalletBalanceResponse.Type, getWalletBalanceResponse.PageIndex, getWalletBalanceResponse.PageSize, getWalletBalanceResponse.CurrencyType);

            return Ok(data);
        }

        [HttpPost("GetCashOutCommissionEarned")]
        public IActionResult GetCashOutCommissionEarnedReport(GetBusinessReport getWalletBalanceResponse)
        {
            var data = _reportService.getcashOutCommissionEarnedReport(getWalletBalanceResponse.FromDate, getWalletBalanceResponse.ToDate, getWalletBalanceResponse.Type, getWalletBalanceResponse.PageIndex, getWalletBalanceResponse.PageSize, getWalletBalanceResponse.CurrencyType);

            return Ok(data);
        }

        [HttpPost("GetFloatAccountReport")]
        public IActionResult GetFloatAccountReport(GetFloatAccount getWalletBalanceResponse)
        {
            var data = _reportService.getFloatAccountReport(getWalletBalanceResponse.FromDate, getWalletBalanceResponse.ToDate, getWalletBalanceResponse.TransferType, getWalletBalanceResponse.PageIndex, getWalletBalanceResponse.PageSize, getWalletBalanceResponse.CurrencyType);

            return Ok(data);
        }

        [HttpPost("GetReconciliationReports")]
        public IActionResult GetReconciliationReports(GetReconciliationReport getWalletBalanceResponse)
        {
            var data = _reportService.getReconciliationReport(getWalletBalanceResponse.PageIndex, getWalletBalanceResponse.PageSize, getWalletBalanceResponse.CurrencyType,getWalletBalanceResponse.DateTime, getWalletBalanceResponse.userId);

            return Ok(data);
        }

        [HttpPost("GetReconciliationReportsByMonth")]
        public IActionResult GetReconciliationReportsByMonth(GetReconciliationReport getWalletBalanceResponse)
        {
            var data = _reportService.getReconciliationReportByMonth(
                getWalletBalanceResponse.PageIndex, 
                getWalletBalanceResponse.PageSize, 
                getWalletBalanceResponse.CurrencyType,
                getWalletBalanceResponse.DateTime,
                getWalletBalanceResponse.userId);

            return Ok(data);
        }

        [HttpGet("GetWalletBalancesReport")]
        [AllowAnonymous]
        public IActionResult GeWalletBalances(string role,int currency,int pageIndex,int pageSize)
        {
            var data = _reportService.getWalletBalancesReport(role, currency, pageIndex, pageSize);

            return Ok(data);
        }

    }
}
