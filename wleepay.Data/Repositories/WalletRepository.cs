using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
