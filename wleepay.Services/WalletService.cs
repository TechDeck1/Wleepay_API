using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;
using System.Linq;
using wleepay.Core.Enums;
using Microsoft.SqlServer.Management.Smo;
using wleepay.Core.DTO.Response;

namespace wleepay.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WalletService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Wallet> AddOrUpdateWallet(Wallet walletInfo)
        {
            Wallet wallet = null;
            if (walletInfo.Id > 0)
            {
                wallet = await _unitOfWork.Wallet.GetByIdAsync((int)walletInfo.Id);
                if (wallet != null)
                    wallet = walletInfo;
            }
            else
            {
                await _unitOfWork.Wallet.AddAsync(walletInfo);
                wallet = walletInfo;
            }
            await _unitOfWork.CommitAsync();
            return wallet;
        }

        public async Task<Wallet> UpdateWalletBalance(int walletId, double balance)
        {
            Wallet wallet = await _unitOfWork.Wallet.GetByIdAsync(walletId);
            if (wallet == null)
                return wallet;

            wallet.Balance = (wallet.Balance + (balance));
            if (wallet.Balance < 0)
                return null;

            await _unitOfWork.CommitAsync();

            return wallet;
        }

        public async Task<Wallet> GetWallet(long id)
        {
            return await _unitOfWork.Wallet.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Wallet>> GetWalletsByUserId(string userId, int currencyId = 0)
        {
            return await _unitOfWork.Wallet.GetWalletsByUserId(userId, currencyId);
        }

        public async Task<IEnumerable<Wallet>> CreateCustomerWallet(string userId)
        {
            List<Wallet> newWallet = new List<Wallet>();
            if (!string.IsNullOrEmpty(userId))
            {
                //create wallet for different currencies(For now it's USD and LRD)

                var liberainWallet = new Wallet()
                {
                    Balance = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Type = (int)WalletTypes.MemberAccount,
                    CurrencyId = (int)Currencies.LRD,
                    UserId = userId
                };

                newWallet.Add(liberainWallet);

                var usdWallet = new Wallet()
                {
                    Balance = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Type = (int)WalletTypes.MemberAccount,
                    CurrencyId = (int)Currencies.USD,
                    UserId = userId
                };

                newWallet.Add(usdWallet);

                await _unitOfWork.Wallet.AddRangeAsync(newWallet);
                await _unitOfWork.CommitAsync();
            }

            return newWallet;
        }

        public async Task<IEnumerable<Wallet>> CreateAgentWallet(string userId)
        {
            List<Wallet> newWallet = new List<Wallet>();
            if (!string.IsNullOrEmpty(userId))
            {
                //create wallet for different currencies(For now it's USD and LRD)

                var liberainWallet = new Wallet()
                {
                    Balance = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Type = (int)WalletTypes.AgentAccount,
                    CurrencyId = (int)Currencies.LRD,
                    UserId = userId
                };

                newWallet.Add(liberainWallet);

                var usdWallet = new Wallet()
                {
                    Balance = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Type = (int)WalletTypes.AgentAccount,
                    CurrencyId = (int)Currencies.USD,
                    UserId = userId
                };

                newWallet.Add(usdWallet);

                var liberainCommissionWallet = new Wallet()
                {
                    Balance = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Type = (int)WalletTypes.AgentComission,
                    CurrencyId = (int)Currencies.LRD,
                    UserId = userId
                };

                newWallet.Add(liberainCommissionWallet);

                var usdCommissionWallet = new Wallet()
                {
                    Balance = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Type = (int)WalletTypes.AgentComission,
                    CurrencyId = (int)Currencies.USD,
                    UserId = userId
                };

                newWallet.Add(usdCommissionWallet);

                await _unitOfWork.Wallet.AddRangeAsync(newWallet);
                await _unitOfWork.CommitAsync();
            }

            return newWallet;
        }

        public async Task<Wallet> GetUserDetailsByWalletIdAndCurrency(long walletId)
        {
            return await _unitOfWork.Wallet.GetUserDetailsByWalletIdAndCurrency(walletId);
        }

        public async Task<IEnumerable<Wallet>> CreateSystemAdminWallet(string userId)
        {
            List<Wallet> newWallet = new List<Wallet>();
            if (!string.IsNullOrEmpty(userId))
            {
                //create wallet for different currencies(For now it's USD and LRD)

                var liberainWallet = new Wallet()
                {
                    Balance = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Type = (int)WalletTypes.SystemWallet,
                    CurrencyId = (int)Currencies.LRD,
                    UserId = userId
                };

                newWallet.Add(liberainWallet);

                var usdWallet = new Wallet()
                {
                    Balance = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Type = (int)WalletTypes.SystemWallet,
                    CurrencyId = (int)Currencies.USD,
                    UserId = userId
                };

                newWallet.Add(usdWallet);

                await _unitOfWork.Wallet.AddRangeAsync(newWallet);
                await _unitOfWork.CommitAsync();
            }

            return newWallet;

        }

        public async Task<IEnumerable<Wallet>> GetWalletByWalletTypeId(long walletTypeId)
        {
            return await _unitOfWork.Wallet.GetWalletByWalletTypeId(walletTypeId);
        }
        public async Task<IEnumerable<Wallet>> GetWalletByWalletTypeIdAndUserId(string userId, long walletTypeId)
        {
            return await _unitOfWork.Wallet.GetWalletByWalletTypeIdAndUserId(userId, walletTypeId);
        }

        public async Task<Wallet> SetCashOnHand(double amount, int currencyId, int WalletTypeId)
        {
            return await _unitOfWork.Wallet.SetCashOnHand(amount,currencyId,WalletTypeId);
        }
        public async Task<IList<WalletAmountHistoryByMonths>> GetWalletHistoryByWalletTypeAndCurrency(int walletType, int currencyId)
        {
            return await _unitOfWork.Wallet.GetWalletHistoryByWalletTypeAndCurrency(walletType, currencyId);
        }
    }
}
