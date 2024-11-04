using Microsoft.EntityFrameworkCore;
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

        public async Task<WalletsBeginingBalance> getStartingWalletBalance(DateTime reportStartDate, int walletType)
        {
            var beginningBalanceLD = await WPContext.Wallet
                .Where(t => t.CreatedDate < reportStartDate && t.CurrencyId == 1 && t.Type == walletType)
                .SumAsync(t => t.Balance);

            var beginningBalanceUSD = await WPContext.Wallet
                .Where(t => t.CreatedDate < reportStartDate && t.CurrencyId == 2 && t.Type == walletType)
                .SumAsync(t => t.Balance);

            return (new WalletsBeginingBalance { BeginningBalanceLD = beginningBalanceLD, BeginningBalanceUSD = beginningBalanceUSD });

        }

        public async Task<WalletsEndingBalance> getEndingWalletBalance(DateTime reportEndDate, int walletType)
        {
            var endingBalanceLD = await WPContext.Wallet
                .Where(t => t.CreatedDate < reportEndDate && t.CurrencyId == 1 && t.Type == walletType)
                .SumAsync(t => t.Balance);

            var endingBalanceUSD = await WPContext.Wallet
                .Where(t => t.CreatedDate < reportEndDate && t.CurrencyId == 2 && t.Type == walletType)
                .SumAsync(t => t.Balance);

            return (new WalletsEndingBalance { EndingBalanceLD = endingBalanceLD, EndingBalanceUSD = endingBalanceUSD });

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

        public async Task<DebitsByWleepay> getBusinessAccountReport()
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
    }
}
