using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Services;
using wleepay.Core;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Services
{
    public class WleeSendUserDetailService: IWleeSendUserDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionService _transactionService;
        public WleeSendUserDetailService(IUnitOfWork unitOfWork, ITransactionService transactionService)
        {
            this._unitOfWork = unitOfWork;
            this._transactionService = transactionService;
        }

        public async Task<WleeSendUserDetails> AddOrUpdateUserDetails(WleeSendUserDetails detail)
        {
            WleeSendUserDetails userDetail = new WleeSendUserDetails();
            if (detail.Id > 0)
            {
                if (userDetail != null)
                {
                    userDetail = detail;
                    userDetail.UpdatedDate = DateTime.Now;
                }
            }
            else
            {
                detail.CreatedDate = DateTime.Now;
                await _unitOfWork.WleeSendUserDetail.AddAsync(detail);
            }

            await _unitOfWork.CommitAsync();
            var data = await _unitOfWork.WleeSendUserDetail.GetUserDetailsById(detail.UserId);
            return await _unitOfWork.WleeSendUserDetail.GetUserDetailsById(detail.UserId);
        }

        public async Task<WleeSendUserDetails> GetWleeSendUserDetailsByPhone(string phone)
        {
            return await _unitOfWork.WleeSendUserDetail.GetUserDetailsByPhone(phone);
        }

        public async Task<WleeSendUserDetails> UpdateUserLoggedInStatus(string userId, bool status, int? failedLoginAttemps)
        {
            var userToUpdate = await _unitOfWork.WleeSendUserDetail.GetUserDetailsById(userId);
            if (userToUpdate != null)
            {
                userToUpdate.FailedLoginAttempt = failedLoginAttemps ?? 0;
                userToUpdate.UpdatedDate = DateTime.Now;
                userToUpdate.IsLoggedIn = status;
                await _unitOfWork.CommitAsync();
            }
            return userToUpdate;
        }

        public async Task<bool> IsPhoneAlreadyRegistered(string phone)
        {
            return await _unitOfWork.WleeSendUserDetail.IsPhoneAlreadyRegistered(phone);
        }

        public async Task<bool> IsAccountAlreadyExists(string accountNumber)
        {
            return await _unitOfWork.WleeSendUserDetail.IsAccountAlreadyExists(accountNumber);
        }

        public async Task<WleeSendUserDetails> GetUserDetailsById(string userId)
        {
            var userDetails = await _unitOfWork.WleeSendUserDetail.GetUserDetailsById(userId);
            return userDetails;
        }
    }
}
