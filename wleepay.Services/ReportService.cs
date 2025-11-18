using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using static DevExpress.XtraPrinting.Native.PageSizeInfo;

namespace wleepay.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<UserDetailsResponseResource> GetReportOfUserByStatusAsync(int role, int status, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _unitOfWork.UserDetail.GetReportOfUserByStatusAsync(role, status, pageIndex, pageSize);
            return result;

        }

        public async Task<TicketsResponse> GetReportOfUserByOpenTicketsAsync(int role, int status, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _unitOfWork.UserDetail.GetReportOfUserByOpenTicketsAsync(role, status, pageIndex, pageSize);
            return result;

        }

        public async Task<UserDetailsResponseResource> GetInActiveSubscriberListAsync(int role)
        {
            var result = await _unitOfWork.UserDetail.GetInActiveSubscriberAndMemberListAsync(role);
            return result;
        }

        public async Task<UserDetailsResponseResource> GetActiveSubscriberListAsync(int role)
        {
            var result = await _unitOfWork.UserDetail.GetActiveSubscriberAndMemberListAsync(role);
            return result;
        }

        public async Task<UserDetailsResponseResource> GetSubscriberAndMemberListByWalletBalanceAsync(int WalletType, int role, int balance, int walletType)
        {
            var result = await _unitOfWork.UserDetail.GetSubscriberAndMemberListByWalletBalanceAsync(WalletType, role,balance, walletType);
            return result;
        }

        public async Task<UserDetailsResponseResource> GetReportBySubscribersKYCLevelAsync(int level, int role)
        {

            var result = await _unitOfWork.UserDetail.GetReportBySubscribersKYCLevelAsync(level, role);
            return result;
        }

        public async Task<UserDetailsResponseResource> GetAllActiveAccountsAsync(int role)
        {
            var result = await _unitOfWork.UserDetail.GetAllActiveAccountsAsync(role);
            return result;

        }

        public async Task<UserDetailsResponseResource> GetAccountOpenedByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            var result = await _unitOfWork.UserDetail.GetAccountOpenedByDateRangeAsync(fromDate,toDate);
            return result;

        }


        public async Task<int> GetSubscribersCountByCountryAsync(int countryId)
        {
            var result = await _unitOfWork.UserDetail.GetSubscribersCountByCountryAsync(countryId);
            return result;

        }

        public async Task<int> GetSubscribersCountByAgeAsync(int minAge,int maxAge)
        {
            var result = await _unitOfWork.UserDetail.GetSubscribersCountByAgeAsync(minAge,maxAge);
            return result;

        }
        public async Task<TransactionResponseResource> TransactionCountSummary(int role, int transaferType, DateTime fromDate, DateTime toDate)
        {
            var result = await _unitOfWork.Transaction.TransactionCountSummary(role , transaferType, fromDate , toDate);
            return result;

        }

        public async Task<SochitelTransactionResponseResource> SochitelCommisionTransactionSummary(int role, string transaferType, DateTime fromDate, DateTime toDate)
        {
            var result = await _unitOfWork.Transaction.SochitelCommisionTransactionSummary(role, transaferType, fromDate, toDate);
            return result;

        }
        public async Task<WalletsBeginingBalance> getStartingWalletBalance(DateTime reportStartDate, int walletType,int currencyId)
        {
            var result = await _unitOfWork.Wallet.getStartingWalletBalance(reportStartDate, walletType, currencyId);
            return result;
        }

        public async Task<WalletsEndingBalance> getEndingWalletBalance(DateTime reportStartDate, int walletType, int currencyId)
        {
            var result = await _unitOfWork.Wallet.getEndingWalletBalance(reportStartDate,walletType,currencyId);
            return result;
        }

        public async Task<WalletsBalanceResponse> getWalletBalance(DateTime reportStartDate, DateTime reportEndDate, int walletType, int currencyId)
        {
            var result = await _unitOfWork.Wallet.getWalletBalance(reportStartDate, reportEndDate, walletType, currencyId);
            return result;
        }

        public async Task<CreditsByWleepay> getCreditsForAgentAndMemberByWleepay()
        {
            var result = await _unitOfWork.Wallet.getCreditsForAgentAndMemberByWleepay();
            return result;
        }
        public async Task<DebitsByWleepay> getDebitsForAgentAndMemberByWleepay()
        {

            var result = await _unitOfWork.Wallet.getDebitsForAgentAndMemberByWleepay();
            return result;
        }

        public CashOnHandReponse getCashOnHandByWleepay(DateTime reportStartDate, DateTime reportEndDate, int walletType, int currencyType, int PageSize, int PageIndex)
        {
            var result = _unitOfWork.Wallet.getCashHandByWleepay(reportStartDate, reportEndDate, walletType, currencyType,PageSize,PageIndex);
            return result;
        }

        public GetCashOutCommissionEarnedResponse getcashOutCommissionEarnedReport(DateTime reportStartDate, DateTime reportEndDate, string type, int pageIndex, int pageSize, int currencyId)
        {
            var result = _unitOfWork.Wallet.getcashOutCommissionEarnedReport(reportStartDate, reportEndDate, type, pageIndex,pageSize,currencyId);
            return result;
        }

        public GetBillPaymentCommissionEarnedResponse getBillPaymentCommissionEarnedReport(DateTime reportStartDate, DateTime reportEndDate, string type, int pageIndex, int pageSize, int currencyId)
        {
            var result = _unitOfWork.Wallet.getBillPaymentCommissionEarnedReport(reportStartDate, reportEndDate, type,pageIndex,pageSize, currencyId);
            return result;
        }

        public GetFloatAccountResponse getFloatAccountReport(DateTime reportStartDate, DateTime reportEndDate, int walletType, int pageIndex, int pageSize, int currencyId)
        {
            var result = _unitOfWork.Wallet.getFloatAccountReport(reportStartDate, reportEndDate, walletType, pageIndex, pageSize, currencyId);
            return result;
        }

        public GetReconciliationResponse getReconciliationReport(int pageIndex, int pageSize, int currencyId, DateTime reportStartDate, string userId)
        {
            var result = _unitOfWork.Wallet.getReconciliationReport(pageIndex, pageSize, currencyId, reportStartDate,userId);
            return result;
        }

        public List<GetReconciliationMonthlyBalance> getReconciliationReportByMonth(int pageIndex, int pageSize, int currencyId, DateTime reportStartDate, string userId)
        {
            var result = _unitOfWork.Wallet.getReconciliationReportByMonth(pageIndex, pageSize, currencyId, reportStartDate, userId);
            return result;
        }

        public GetWalletBalances getWalletBalancesReport(string role, int currency, int pageIndex, int pageSize)
        {
            var result = _unitOfWork.Wallet.getWalletBalancesReport(role, currency, pageIndex, pageSize);
            return result;
        }

        public async Task<IEnumerable<LecTransactionReportResponse>> GetLecTransactionsByDates(DateTime startDate, DateTime endDate)
        {
            return await _unitOfWork.Transaction.GetTransactionsByDates(startDate, endDate);
        }

    }
}
