using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Task<IEnumerable<Wallet>> GetWalletsByUserId(string userId, int currencyId = 0, int typpe =0);
        Task<Wallet>  GetUserByWalletIdAndTransferType(long userId, int type);
        Task<Wallet> GetUserDetailsByWalletIdAndCurrency(long walletId);
        Task<IEnumerable<Wallet>> GetWalletByWalletTypeId(long walletTypeId);
        Task<IEnumerable<Wallet>> GetWalletByWalletTypeIdAndUserId(string userId,long walletTypeId);
        Task<Wallet> SetCashOnHand(double amount, int currencyId, int WalletTypeId);
        Task<IList<WalletAmountHistoryByMonths>> GetWalletHistoryByWalletTypeAndCurrency(int walletType, int currencyId);
        Task<WalletsBeginingBalance> getStartingWalletBalance(DateTime reportStartDate, int walletType, int currencyId);
        Task<WalletsEndingBalance> getEndingWalletBalance(DateTime reportStartDate, int walletType, int currencyId);
        Task<CreditsByWleepay> getCreditsForAgentAndMemberByWleepay();
        Task<DebitsByWleepay> getDebitsForAgentAndMemberByWleepay();
        CashOnHandReponse getCashHandByWleepay(DateTime reportStartDate, DateTime reportEndDate, int walletType,int currencyType, int PageSize, int PageIndex);

        GetCashOutCommissionEarnedResponse getcashOutCommissionEarnedReport(DateTime reportStartDate, DateTime reportEndDate, string type, int pageIndex, int pageSize, int currencyId);
        GetBillPaymentCommissionEarnedResponse getBillPaymentCommissionEarnedReport(DateTime reportStartDate, DateTime reportEndDate, string type, int pageIndex, int pageSize, int currencyId);

        GetFloatAccountResponse getFloatAccountReport(DateTime reportStartDate, DateTime reportEndDate, int walletType, int pageIndex, int pageSize, int currencyId);
        GetReconciliationResponse getReconciliationReport(int pageIndex, int pageSize, int currencyId, DateTime reportStartDate, string userId);
        List<GetReconciliationMonthlyBalance> getReconciliationReportByMonth(int pageIndex, int pageSize, int currencyId, DateTime reportStartDate, string userId);
    }
}
