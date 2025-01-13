using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;
using static DevExpress.XtraPrinting.Native.PageSizeInfo;

namespace wleepay.Data.Repositories
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(DbContext context):base(context)
        {

        }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<IEnumerable<Wallet>> GetWalletsByUserId(string userId, int currencyId = 0,int type = 0)
        {
            var wallets = WPContext.Wallet.Where(w => w.UserId == userId).AsQueryable();
            if (currencyId > 0)
                wallets = wallets.Where(w => w.CurrencyId == currencyId);
            if (type > 0)
                wallets = wallets.Where(w => w.Type == type);
            var walletList = await wallets
                .Include(w=>w.Currency)
                .Include(w=>w.WalletType)
                .ToListAsync();

            return walletList;
        }
        
        public async Task<Wallet> GetUserByWalletIdAndTransferType(long walletId, int type)
        {
            var wallet = WPContext.Wallet.Where(w => w.Id == walletId && w.Type == type).AsQueryable();
            var walletDetail = await wallet
                .Include(w => w.Currency)
                .Include(w => w.WalletType)
                .FirstOrDefaultAsync();

            return walletDetail;
        }
        
        public async Task<Wallet> GetUserDetailsByWalletIdAndCurrency(long walletId)
        {
            return await WPContext.Wallet.Where(w => w.Id == walletId)
                .Include(s=>s.Currency)
                .Include(s => s.ApplicationUser)
                .ThenInclude(s => s.UserDetail)
                .FirstOrDefaultAsync();
        }
        
        public async Task<IEnumerable<Wallet>> GetWalletsBy(string userId, int currencyId = 0)
        {
            var wallets = WPContext.Wallet.Where(w => w.UserId == userId).AsQueryable();
            if (currencyId > 0)
                wallets = wallets.Where(w => w.CurrencyId == currencyId);

            var walletList = await wallets
                .Include(w => w.Currency)
                .Include(w => w.WalletType)
                .ToListAsync();

            return walletList;
        }

        public async Task<IEnumerable<Wallet>> GetWalletByWalletTypeId(long walletTypeId)
        {
            var wallet = WPContext.Wallet.Where(w => w.Type == walletTypeId).AsQueryable();
            var walletDetail = await wallet
                .Include(w => w.Currency)
                .Include(w => w.WalletType)
                .ToListAsync();

            return walletDetail;
        }

        public async Task<IEnumerable<Wallet>> GetWalletByWalletTypeIdAndUserId(string userId, long walletTypeId)
        {
            var wallet = WPContext.Wallet.Where(w => w.Type == walletTypeId && w.UserId == userId).AsQueryable();
            var walletDetail = await wallet
                .Include(w => w.Currency)
                .Include(w => w.WalletType)
                .ToListAsync();

            return walletDetail;
        }

        public async Task<Wallet> SetCashOnHand(double amount, int currencyId, int WalletTypeId)
        {
            var wallet = await WPContext.Wallet.Where(w => w.Type == WalletTypeId && w.CurrencyId == currencyId).FirstOrDefaultAsync();
            if(wallet != null)
            {
                wallet.Balance = amount; 
                wallet.CurrencyId=currencyId;
            }
            await WPContext.SaveChangesAsync();

            return wallet;
        }

        public async Task<IList<WalletAmountHistoryByMonths>> GetWalletHistoryByWalletTypeAndCurrency(int walletType, int currencyId)
        {

            // Fetch the relevant data first
            var transactions = WPContext.Transactions
                .Include(t => t.ToWallet)
                .Where(t => t.Status == 3 && t.CreatedDate >= DateTime.Now.AddMonths(-6).Date && t.ToWallet.CurrencyId == currencyId && t.ToWallet.Type == walletType)
                .ToList();  // Fetch data into memory

            // Perform grouping and conditional sum in memory
            var finalResult = transactions
                .GroupBy(x => new
                {
                    Year = x.CreatedDate.Year,
                    Month = x.CreatedDate.Month,
                })
                .Select(g => new WalletAmountHistoryByMonths
                {
                    Name = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy"), // Format after grouping
                    Value = g.Sum(x => x.Amount).ToString(),  
                    TotalCount = g.Count(),
                })
                .OrderBy(g => g.Name)
                .ToList();

            return finalResult;

        }

        public async Task<WalletsBeginingBalance> getStartingWalletBalance(DateTime reportStartDate, int walletType,int currencyId)
        {
            var transactions = await WPContext.Transactions
                .Where(t => t.CreatedDate.Date >= reportStartDate.Date && t.FromWallet.CurrencyId == currencyId && t.FromWallet.Type == walletType)
                .SumAsync(t => t.Amount);

            var currentBalance = await WPContext.Wallet
                .Where(t => t.CurrencyId == currencyId && t.Type == walletType)
                .SumAsync(t => t.Balance);

            return (new WalletsBeginingBalance { BeginningBalance = currentBalance - transactions });
        }

        public async Task<WalletsEndingBalance> getEndingWalletBalance(DateTime reportEndDate, int walletType, int currencyId)
        {
            var transactions = await WPContext.Transactions
                    .Where(t => t.CreatedDate.Date >= reportEndDate.Date && t.FromWallet.CurrencyId == currencyId && t.FromWallet.Type == walletType)
                    .SumAsync(t => t.Amount);

            var currentBalance = await WPContext.Wallet
                .Where(t => t.CurrencyId == currencyId && t.Type == walletType)
                .SumAsync(t => t.Balance);

            return (new WalletsEndingBalance { EndingBalance = currentBalance - transactions });
        }

        public async Task<CreditsByWleepay> getCreditsForAgentAndMemberByWleepay()
        {
            var creditAgentTopupsLD = await WPContext.Transactions
            .Where(t => t.TransferTypeId == TransferTypes.FillAgent.GetHashCode() && t.FromWallet.CurrencyId == 1 && t.FromWallet.Type == WalletTypes.AgentAccount.GetHashCode())
            .SumAsync(t => t.Amount);

            var creditSubscriberTopupsLD = await WPContext.Transactions
            .Where(t => t.TransferTypeId == TransferTypes.MemberTopUpByWleePay.GetHashCode() && t.FromWallet.CurrencyId == 1 && t.FromWallet.Type == WalletTypes.MemberAccount.GetHashCode())
            .SumAsync(t => t.Amount);

            var creditAgentTopupsUSD = await WPContext.Transactions
            .Where(t => t.TransferTypeId == TransferTypes.FillAgent.GetHashCode() && t.FromWallet.CurrencyId == 2 && t.FromWallet.Type == WalletTypes.AgentAccount.GetHashCode())
            .SumAsync(t => t.Amount);

            var creditSubscriberTopupsUSD = await WPContext.Transactions
            .Where(t => t.TransferTypeId == TransferTypes.MemberTopUpByWleePay.GetHashCode() && t.FromWallet.CurrencyId == 2 && t.FromWallet.Type == WalletTypes.MemberAccount.GetHashCode())
            .SumAsync(t => t.Amount);

            return new CreditsByWleepay
            {
                LDCreditToAgent = creditAgentTopupsLD,
                LDCreditToMember = creditSubscriberTopupsLD,
                USDCreditToAgent = creditAgentTopupsUSD,
                USDCreditToMember = creditSubscriberTopupsUSD

            };
        }

        public async Task<DebitsByWleepay> getDebitsForAgentAndMemberByWleepay()
        {
            var LDdebitSubscriberCashouts = await WPContext.Transactions
            .Where(t => t.TransferTypeId == TransferTypes.MemberCashOutByWleePay.GetHashCode() && t.FromWallet.CurrencyId == 1 && t.FromWallet.Type == WalletTypes.MemberAccount.GetHashCode())
            .SumAsync(t => t.Amount);

            var USDdebitSubscriberCashouts = await WPContext.Transactions
            .Where(t => t.TransferTypeId == TransferTypes.MemberCashOutByWleePay.GetHashCode() && t.FromWallet.CurrencyId == 2 && t.FromWallet.Type == WalletTypes.MemberAccount.GetHashCode())
            .SumAsync(t => t.Amount);

            var LDdebitAgentCommissionCashouts = await WPContext.Transactions
            .Where(t => t.TransferTypeId == TransferTypes.AgentCommissionCashout.GetHashCode() && t.FromWallet.CurrencyId == 1 && t.FromWallet.Type == WalletTypes.AgentComission.GetHashCode())
            .SumAsync(t => t.Amount);

            var USDdebitAgentCommissionCashouts = await WPContext.Transactions
            .Where(t => t.TransferTypeId == TransferTypes.AgentCommissionCashout.GetHashCode() && t.FromWallet.CurrencyId == 2 && t.FromWallet.Type == WalletTypes.AgentComission.GetHashCode())
            .SumAsync(t => t.Amount);

            return new DebitsByWleepay
            {
                LDDebitsFromAgent = LDdebitAgentCommissionCashouts,
                LDDebitFromMember = LDdebitSubscriberCashouts,
                USDDebitsFromAgent = USDdebitAgentCommissionCashouts,
                USDDebitsFromMember = USDdebitSubscriberCashouts

            };

        }


        public CashOnHandReponse getCashHandByWleepay(DateTime reportStartDate, DateTime reportEndDate, int walletType, int currencyType, int PageSize, int PageIndex)
        {
            int takeCount = PageSize;
            int skipCount = PageIndex * PageSize;

            var result = WPContext.Transactions
             .Include(s => s.ToWallet.ApplicationUser.UserDetail)
            .Where(t => t.TransferTypeId == walletType
               && t.FromWallet.CurrencyId == currencyType
                      && t.CreatedDate.Date >= reportStartDate.Date
        && t.CreatedDate.Date <= reportEndDate.Date
        );

            var resultList = result.Skip(skipCount).Take(takeCount)
            .ToList();

            return (new CashOnHandReponse
            {
                total = result.Count(),
                transacion = resultList
            });
        }

        public GetCashOutCommissionEarnedResponse getcashOutCommissionEarnedReport(DateTime reportStartDate, DateTime reportEndDate, string type, int pageIndex, int pageSize, int currencyId)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;

            var cashOutCommission = WPContext.Transactions
            .Include(u => u.FromWallet.ApplicationUser.UserDetail)
            .Where(t => t.TransferTypeId == TransferTypes.CashOutStandardFeeSplit.GetHashCode()
                        && t.FromWallet.Type == WalletTypes.WleeTax.GetHashCode()
                        && t.FromWallet.CurrencyId == currencyId
                         && t.CreatedDate.Date >= reportStartDate.Date
        && t.CreatedDate.Date <= reportEndDate.Date);

            var cashOut = cashOutCommission.Skip(skipCount).Take(takeCount)
            .ToList();


            return new GetCashOutCommissionEarnedResponse
            {
                Total = cashOutCommission.Count(),
                cashOutCommissionEarned = cashOut
            };

        }

        public GetBillPaymentCommissionEarnedResponse getBillPaymentCommissionEarnedReport(DateTime reportStartDate, DateTime reportEndDate, string type, int pageIndex, int pageSize, int currencyId)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;

            var CreditCommissionEarned = WPContext.SochitelTransactions
             .Include(s => s.Transaction.FromWallet.ApplicationUser)
            .Where(t => t.TransactionType == type
            && t.CreatedDate >= reportStartDate.Date
        && t.CreatedDate <= reportEndDate.Date
        );

            var creditsList = CreditCommissionEarned.Skip(skipCount).Take(takeCount)
              .ToList();



            return new GetBillPaymentCommissionEarnedResponse
            {
               Total = CreditCommissionEarned.Count(),
               billCommissionEarned = creditsList
            };

        }

        public GetFloatAccountResponse getFloatAccountReport(DateTime reportStartDate, DateTime reportEndDate, int walletType, int pageIndex, int pageSize, int currencyId)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;

            var transactions = WPContext.Transactions
             .Include(s => s.ToWallet.ApplicationUser.UserDetail)
            .Where(t => t.TransferTypeId == walletType
               && t.FromWallet.CurrencyId == currencyId
                           && t.CreatedDate.Date >= reportStartDate.Date
        && t.CreatedDate.Date <= reportEndDate.Date
               );

            var cashOut = transactions.Skip(skipCount).Take(takeCount)
            .ToList();


            return new GetFloatAccountResponse
            {
                Total = transactions.Count(),
                Transaction = cashOut
            };
        }

        public GetReconciliationResponse getReconciliationReport(int pageIndex, int pageSize, int currencyId, DateTime reportStartDate, string userId)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;

            var reconciliationResult = WPContext.Wallet.Include(u => u.ApplicationUser).Include(w => w.WalletType)
                .Where(t => t.CurrencyId == currencyId 
                && t.ApplicationUser.UserDetail.UserId == userId 
                && t.ApplicationUser.UserDetail.Status == Status.Active.GetHashCode());

            var reconciliationList = reconciliationResult.Skip(skipCount).Take(takeCount)
              .ToList();

            return new GetReconciliationResponse
            {
                Total = reconciliationResult.Count(),
                reconciliations = reconciliationList
            };
        }

        public List<GetReconciliationMonthlyBalance> getReconciliationReportByMonth(int pageIndex, int pageSize, int currencyId, DateTime reportStartDate, string userId)
        {
            var currentWalletBalances = WPContext.Wallet
                 .Include(u => u.ApplicationUser)
                 .Include(u => u.WalletType)
                 .Where(t => t.CurrencyId == currencyId
                     && t.ApplicationUser.UserDetail.UserId == userId)
                 .ToList();

            var userEmail = currentWalletBalances
                .Select(w => w.ApplicationUser.Email)
                .FirstOrDefault();

            var walletIds = currentWalletBalances.Select(w => w.Id).ToList();

            var transactions = WPContext.Transactions
                .Include(t => t.FromWallet.ApplicationUser)
                .Where(t => walletIds.Contains(t.FromWalletId ?? 0)
                    && t.FromWallet.CurrencyId == currencyId
                    && t.FromWallet.ApplicationUser.UserDetail.UserId == userId
                    && t.CreatedDate >= DateTime.Now.AddMonths(-6))
                .ToList();

            var lastSixMonthsBalances = new List<GetReconciliationMonthlyBalance>();

            foreach (var wallet in currentWalletBalances)
            {
                double walletBalance = wallet.Balance;

                for (int i = 0; i < 6; i++) // Iterate over 6 months
                {
                    var monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-i);
                    var monthEnd = monthStart.AddMonths(1);

                    // Filter transactions for this wallet and month
                    var monthlyTransactions = transactions
                        .Where(t => t.FromWalletId != null && t.FromWalletId == wallet.Id && t.CreatedDate.Month >= monthStart.Month && t.CreatedDate.Month < monthEnd.Month)
                        .ToList();

                    var monthlyTransactionSum = monthlyTransactions
                        .Sum(t => (t.TransferTypeId == 5 || t.TransferTypeId == 1) ? t.Amount : -t.Amount);

                    // Add to result list BEFORE updating the wallet balance
                    lastSixMonthsBalances.Add(new GetReconciliationMonthlyBalance
                    {
                        WalletName = wallet.WalletType.Name, // Include Wallet ID
                        Month = monthStart.ToString("MMMM yyyy"), // Format as "Month Year"
                        Balance = walletBalance,
                        UserEmail = userEmail,
                    });

                    // Update the wallet balance to include this month's transactions
                    walletBalance += monthlyTransactionSum;
                }
            }

            return lastSixMonthsBalances;

        }
    }
}
