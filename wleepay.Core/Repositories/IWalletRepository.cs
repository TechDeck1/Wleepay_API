using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Task<IEnumerable<Wallet>> GetWalletsByUserId(string userId, int currencyId = 0, int typpe =0);
        Task<Wallet>  GetUserByWalletIdAndTransferType(long userId, int type);
        Task<Wallet> GetUserDetailsByWalletIdAndCurrency(long walletId);
        Task<IEnumerable<Wallet>> GetWalletByWalletTypeId(long walletTypeId);

    }
}
