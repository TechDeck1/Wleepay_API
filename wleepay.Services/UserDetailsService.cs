using MailKit.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class UserDetailsService : IUserDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionService _transactionService;
        public UserDetailsService(IUnitOfWork unitOfWork, ITransactionService transactionService)
        {
            this._unitOfWork = unitOfWork;
            this._transactionService = transactionService;
        }
        public async Task<UserDetail> AddOrUpdateUserDetails(UserDetail detail)
        {
            UserDetail userDetail = new UserDetail();
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
                await _unitOfWork.UserDetail.AddAsync(detail);
            }

            await _unitOfWork.CommitAsync();
            var data =await _unitOfWork.UserDetail.GetUserDetailsById(detail.UserId);
            return await _unitOfWork.UserDetail.GetUserDetailsById(detail.UserId);
        }

        public async Task<UserDetail> GetUserDetailsById(string userId)
        {
            var userDetails= await _unitOfWork.UserDetail.GetUserDetailsById(userId);
            return userDetails;
        }
        public async Task<ApplicationUser> GetApplicationUserById(string userId)
        {
            return await _unitOfWork.UserDetail.GetApplicationUser(userId, string.Empty, string.Empty, true, true);
        }

        public async Task<UserDetail> GetUserDetailsByPhone(string phone)
        {
            return await _unitOfWork.UserDetail.GetUserDetailsByPhone(phone);
        }

        public async Task<bool> IsPhoneAlreadyRegistered(string phone)
        {
            return await _unitOfWork.UserDetail.IsPhoneAlreadyRegistered(phone);
        }

        public async Task<UserDetail> UpdateUserLoggedInStatus(string userId, bool status)
        {
            var userToUpdate = await _unitOfWork.UserDetail.GetUserDetailsById(userId);
            if (userToUpdate != null)
            {
                userToUpdate.UpdatedDate = DateTime.Now;
                userToUpdate.IsLoggedIn = status;
                await _unitOfWork.CommitAsync();
            }
            return userToUpdate;
        }

        public async Task<UserDetail> UpdateUserPhoneNumber(string userId, string phoneNumber)
        {
            var userToUpdate = await _unitOfWork.UserDetail.GetUserDetailsById(userId);
            if (userToUpdate != null)
            {
                userToUpdate.UpdatedDate = DateTime.Now;
                userToUpdate.PhoneNumber = phoneNumber;
                await _unitOfWork.CommitAsync();
            }
            return userToUpdate;
        }

        public async Task<UserDetail> UpdateUserKYCStatus(string userId, int statusId)
        {
            var userToUpdate = await _unitOfWork.UserDetail.GetUserDetailsById(userId);
            if (userToUpdate != null)
            {
                if (statusId == Core.Enums.KYCStatus.Approved.GetHashCode())
                    userToUpdate.KYCLevel = Core.Enums.KYCLevel.L3.GetHashCode();

                else if (statusId == Core.Enums.KYCStatus.Rejected.GetHashCode())
                    userToUpdate.KYCLevel = Core.Enums.KYCLevel.L2.GetHashCode();

                userToUpdate.UpdatedDate = DateTime.Now;
                userToUpdate.KYCStatusId = statusId;
                await _unitOfWork.CommitAsync();
            }
            return userToUpdate;
        }

        public async Task<ApplicationUser> GetUserAccountByPhone(string phone, int role = 0)
        {
            return await _unitOfWork.UserDetail.GetApplicationUser(string.Empty, string.Empty, phone, true, false, role);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUserByRole(int role)
        {
            return await _unitOfWork.UserDetail.GetUserDetailsByRole(role);
        }

        public async Task<UserDetailsResponseResource> GetUsers(Core.Enums.Role role, int pageIndex, int pageSize)
        {
            return await _unitOfWork.UserDetail.GetUsers(role, pageIndex, pageSize);
        }

        public async Task<ApplicationUser> GetUserAccountByName(string name, int role = 0)
        {
            return await _unitOfWork.UserDetail.GetApplicationUser(string.Empty, name, string.Empty, true);
        }

        public async Task<bool> IsAccountAlreadyExists(string accountNumber)
        {
            return await _unitOfWork.UserDetail.IsAccountAlreadyExists(accountNumber);
        }

        public async Task<IEnumerable<Transaction>> GetIncomingOutGoingPaymentRequest(string userId, Core.Enums.PaymentRequestType type, Core.Enums.TransactionStatus transactionStatus,int pageSize, int pageIndex) {
            var user = await _unitOfWork.UserDetail.GetApplicationUser(userId, string.Empty, string.Empty, true);
            if (user == null)
                return null;
            //get user wallets

            var paymentRequest = await _transactionService.GetPaymentRequestByStatus(user.Wallets.Select(s => s.Id), type, transactionStatus,  pageSize,  pageIndex);

            return paymentRequest;
        }
      
        public async Task<UserDetail> UpdateUserVerificationCode(string userId, string VerificationCode)
        {
            var userToUpdate = await _unitOfWork.UserDetail.GetUserDetailsById(userId);
            if (userToUpdate != null)
            {
                userToUpdate.VerificationCode = VerificationCode;
                userToUpdate.VerificationExpiry = DateTime.UtcNow;
                await _unitOfWork.CommitAsync();
            }
            return userToUpdate;
        }

        public async Task<UserDetail> UpdateUserStatus(string userId, int status )
        {
            var userToUpdate = await _unitOfWork.UserDetail.GetUserDetailsById(userId);
            if (userToUpdate != null)
            {
                userToUpdate.UpdatedDate = DateTime.Now;
                userToUpdate.Status = status;
                await _unitOfWork.CommitAsync();
            }
            return userToUpdate;
        }

        public async Task<UserDetailsResponseResource> GetAllUsersById(Core.Enums.Role role, int pageIndex, int pageSize, string createdBy)
        {
            return await _unitOfWork.UserDetail.GetAllUsersById(role, pageIndex, pageSize, createdBy);
        }

        public async Task<UserDetailsResponseResource> SearchUsersByEmail(string searchText, Core.Enums.Role role, int index, int size)
        {
            return await _unitOfWork.UserDetail.SearchApplicationUserByEmail(searchText, role , index, size );
        }

        public async Task<TransactionAndUserResponseResource> VerifyUserAccountByPhone(string phone)
        {
            return await _unitOfWork.UserDetail.VerifyUserAccountByPhone(phone);
        }

        public async Task<ApplicationUser> GetStaffPermissionsById(string userId)
        {
            return await _unitOfWork.UserDetail.GetStaffPermissionsById(userId);
        }
    }
}
