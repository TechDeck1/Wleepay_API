using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class WleeSendUserDetailRepository : Repository<WleeSendUserDetails>, IWleeSendUserDetailsRepository
    {
        public WleeSendUserDetailRepository(WPContext context): base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }
        public async Task<WleeSendUserDetails> GetUserDetailsById(string userId)
        {
            var userDetails = await WPContext.WleeSendUserDetail.Where(user => user.UserId == userId)
                .Include(s => s.County)
                .Include(s => s.WleeSendUser)
                .FirstOrDefaultAsync();
            return userDetails;
        }

        public async Task<WleeSendUserDetails> GetUserDetailsByPhone(string phone)
        {
            return await WPContext.WleeSendUserDetail.Where(user => user.PhoneNumber == phone)
                .Include(s => s.County)
                .Include(s => s.WleeSendUser)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsAccountAlreadyExists(string accountNumber)
        {
            return await WPContext.UserDetail.AnyAsync(u => u.AccountNumber == accountNumber);
        }

        public async Task<bool> IsPhoneAlreadyRegistered(string phone)
        {
            return await WPContext.UserDetail.AnyAsync(user => user.PhoneNumber == phone);
        }

    }
}
