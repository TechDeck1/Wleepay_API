using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

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

    }
}
