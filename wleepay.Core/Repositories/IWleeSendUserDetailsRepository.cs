using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface IWleeSendUserDetailsRepository : IRepository<WleeSendUserDetails>
    {
        Task<WleeSendUserDetails> GetUserDetailsById(string userId);
        Task<WleeSendUserDetails> GetUserDetailsByPhone(string phone);
        Task<bool> IsAccountAlreadyExists(string accountNumber);
        Task<bool> IsPhoneAlreadyRegistered(string phone);
    }
}
