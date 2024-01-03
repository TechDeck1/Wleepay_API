using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class UserDetailRepository : Repository<UserDetail>, IUserDetailsRepository
    {
        public UserDetailRepository(WPContext context)
            : base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<UserDetail> GetUserDetailsById(string userId)
        {
            return await WPContext.UserDetail.Where(user => user.UserId == userId)
                .Include(s => s.KYCLevelDetail)
                .Include(s => s.KYCStatus)
                .Include(s => s.County)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetUserDetailsByRole(int role)
        {
            return await WPContext.Users
                .Include(s => s.UserRoles)
                    .ThenInclude(s => s.Role)
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCLevelDetail)
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCStatus)
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.County)
                .Include(s => s.Wallets)
                    .ThenInclude(s => s.Currency)
                .Where(user => user.UserRoles == null || user.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString())
                .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers(Core.Enums.Role role, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;

            var result= await WPContext.Users
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCLevelDetail)
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCStatus)
                .Include(s => s.UserRoles)
                    .ThenInclude(s => s.Role)
                .Include(s => s.Address)
                .OrderByDescending(w => w.UserDetail.CreatedDate)
                .Where(s=>s.UserRoles.FirstOrDefault().Role.Name == role.ToString())
                .Skip(skipCount)
                .Take(takeCount)
                //.Include(s => s.UserDetail)
                //    .ThenInclude(s => s.County)
                //.Include(s => s.Wallets)
                //    .ThenInclude(s => s.Currency)
                .ToListAsync();

            return result;
        }

        public async Task<ApplicationUser> GetApplicationUser(string userId = "", string name = "", string phoneNumber = "", bool fetchWallet = false, bool fetchKYCDoc = false, int role = 0)
        {
            var appUserQuery = WPContext.Users
                .Include(s => s.UserRoles)
                    .ThenInclude(s => s.Role)
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCLevelDetail)
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCStatus)
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.County).AsQueryable();

            if (fetchWallet)
            {
                appUserQuery = appUserQuery
                    .Include(s => s.Wallets)
                        .ThenInclude(s => s.Currency)
                    .Include(s => s.Wallets)
                        .ThenInclude(s => s.WalletType);
            }


            if (fetchKYCDoc)
            {
                appUserQuery = appUserQuery.Include(s => s.KYCdocuments)
                    .ThenInclude(s => s.DocumentType);
            }


            if (!string.IsNullOrEmpty(userId))
                appUserQuery = appUserQuery.Where(user => user.Id == userId);
            else if (!string.IsNullOrEmpty(name))
                appUserQuery = appUserQuery.Where(user => user.UserDetail.FirstName == name);
            else
                appUserQuery = appUserQuery.Where(user => user.UserDetail.PhoneNumber == phoneNumber);

            if (role > 0)
                appUserQuery = appUserQuery.Where(user => user.UserRoles == null || user.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString());

            return await appUserQuery.FirstOrDefaultAsync();
        }

        public async Task<UserDetail> GetUserDetailsByPhone(string phone)
        {
            return await WPContext.UserDetail.Where(user => user.PhoneNumber == phone)
                .Include(s => s.KYCStatus)
                .Include(s => s.KYCLevelDetail)
                .Include(s => s.County)
                .Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsPhoneAlreadyRegistered(string phone)
        {
            return await WPContext.UserDetail.AnyAsync(user => user.PhoneNumber == phone);
        }

        public async Task<bool> IsAccountAlreadyExists(string accountNumber)
        {
            return await WPContext.UserDetail.AnyAsync(u => u.AccountNumber == accountNumber);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersById(Core.Enums.Role role, int pageIndex = 0, int pageSize = 10, string createdBy="")
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;

            var result = await WPContext.Users
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCLevelDetail)
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCStatus)
                .Include(s => s.UserRoles)
                    .ThenInclude(s => s.Role)
                .Include(s => s.Address)
                .OrderByDescending(w => w.UserDetail.FirstName)
                .Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString() && s.UserDetail.CreatedBy==createdBy)
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();

            return result;
        }

    }
}
