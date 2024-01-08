using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IUserDetailService
    {
        Task<UserDetail> AddOrUpdateUserDetails(UserDetail detail);
        Task<UserDetail> UpdateUserLoggedInStatus(string userId, bool status);
        Task<UserDetail> GetUserDetailsById(string userId);
        Task<IEnumerable<ApplicationUser>> GetUserByRole(int role);
        Task<ApplicationUser> GetApplicationUserById(string userId);
        Task<UserDetail> GetUserDetailsByPhone(string phone);
        //Task<UserDetail> GetUserDetailsByName(string name);
        Task<UserDetail> UpdateUserPhoneNumber(string userId, string phoneNumber);
        Task<UserDetail> UpdateUserKYCStatus(string userId, int statusId);
        Task<bool> IsPhoneAlreadyRegistered(string phone);
        Task<bool> IsAccountAlreadyExists(string accountNumber);
        Task<ApplicationUser> GetUserAccountByPhone(string phone, int role = 0);
        Task<ApplicationUser> GetUserAccountByName(string name, int role = 0);
        Task<UserDetailsResponseResource> GetUsers(Core.Enums.Role role, int pageIndex, int pageSize);
        Task<IEnumerable<Transaction>> GetIncomingOutGoingPaymentRequest(string userId, Core.Enums.PaymentRequestType type, Core.Enums.TransactionStatus transferStatus, int pageSize, int pageIndex);
        Task<UserDetail> UpdateUserVerificationCode(string UserId, string verificationCode);
        Task<UserDetail> UpdateUserStatus(string userId, int status);
        Task<UserDetailsResponseResource> GetAllUsersById(Core.Enums.Role role, int pageIndex, int pageSize, string createdBy);


    }
}
