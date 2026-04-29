using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IWleeSendUserDetailService
    {
        Task<WleeSendUserDetails> GetWleeSendUserDetailsByPhone(string phone);
        Task<WleeSendUserDetails> UpdateUserLoggedInStatus(string userId, bool status, int? failedLoginAttemps);
        Task<bool> IsPhoneAlreadyRegistered(string phone);
        Task<WleeSendUserDetails> AddOrUpdateUserDetails(WleeSendUserDetails detail);
        Task<bool> IsAccountAlreadyExists(string accountNumber);
        Task<WleeSendUserDetails> GetUserDetailsById(string userId);
        
    }
}
