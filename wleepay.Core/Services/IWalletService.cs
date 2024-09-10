using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IWalletService
    {
        Task<IEnumerable<Wallet>> GetWalletsByUserId(string userId, int currencyId = 0);
        Task<Wallet> GetWallet(long id);
        Task<Wallet> AddOrUpdateWallet(Wallet walletInfo);
        Task<Wallet> UpdateWalletBalance(int walletId, double balance);
        Task<IEnumerable<Wallet>> CreateCustomerWallet(string userId);
        Task<IEnumerable<Wallet>> CreateAgentWallet(string userId);
        Task<IEnumerable<Wallet>> CreateSystemAdminWallet(string userId);
        Task<Wallet> GetUserDetailsByWalletIdAndCurrency(long walletId);
        Task<IEnumerable<Wallet>> GetWalletByWalletTypeId(long walletTypeId);
        Task<Wallet> SetCashOnHand(double amount, int currencyId, int WalletTypeId);
        Task<IList<WalletAmountHistoryByMonths>> GetWalletHistoryByWalletTypeAndCurrency(int walletType, int currencyId);


    }
}
