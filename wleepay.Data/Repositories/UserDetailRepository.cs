using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
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
            var userDetails= await WPContext.UserDetail.Where(user => user.UserId == userId)
                .Include(s => s.KYCLevelDetail)
                .Include(s => s.KYCStatus)
                .Include(s => s.County)
                .Include(s => s.ApplicationUser.UsersStaffRole)
                    .ThenInclude(s => s.StaffRoles)
                .FirstOrDefaultAsync();
            return userDetails;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUserDetailsByRole(int role)
        {
            var result = await WPContext.Users
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
                .Where(user => user.UserRoles == null || user.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString() && user.UserDetail.Status==(int)Status.Active)
                .ToListAsync();
            return result;
        }

        public async Task<UserDetailsResponseResource> GetUsers(Core.Enums.Role role, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var result= await WPContext.Users
                .Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString())
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCLevelDetail)
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCStatus)
                .Include(s => s.UserRoles)
                    .ThenInclude(s => s.Role)
                .Include(s => s.Address)
                .Include(s => s.Wallets)
                    .ThenInclude(s => s.Currency)
                .Include(s=> s.UsersStaffRole)
                    .ThenInclude(s=>s.StaffRoles)
                .OrderByDescending(w => w.UserDetail.CreatedDate)
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();

            var totalCount = await WPContext.Users.Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString()).CountAsync();

            UserDetailsResponseResource response = new UserDetailsResponseResource();
            response.UserDetails = result;
            response.TotalCount = totalCount;
            return response;
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
                    .ThenInclude(s => s.County)
                .Include(s => s.UsersStaffRole)
                    .ThenInclude(s => s.StaffRoles)
                .AsQueryable();

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

        public async Task<UserDetailsResponseResource> GetAllUsersById(Core.Enums.Role role, int pageIndex = 0, int pageSize=0, string createdBy="")
        {
            if (pageSize > 0)
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
                    .OrderByDescending(x => x.UserDetail.CreatedDate)
                    .Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString() && s.UserDetail.CreatedBy == createdBy)
                    .Skip(skipCount)
                    .Take(takeCount)
                    .ToListAsync();

                var totalCount = await WPContext.Users.Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString() && s.UserDetail.CreatedBy == createdBy).CountAsync();

                UserDetailsResponseResource response = new UserDetailsResponseResource();
                response.UserDetails = result;
                response.TotalCount = totalCount;

                return response;

            }
            else
            {
                var result = await WPContext.Users
                 .Include(s => s.UserDetail)
                     .ThenInclude(s => s.KYCLevelDetail)
                 .Include(s => s.UserDetail)
                     .ThenInclude(s => s.KYCStatus)
                 .Include(s => s.UserRoles)
                     .ThenInclude(s => s.Role)
                 .Include(s => s.Address)
                 .OrderByDescending(x => x.UserDetail.CreatedDate)
                 .Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString() && s.UserDetail.CreatedBy == createdBy)
                 .ToListAsync();

                var totalCount = await WPContext.Users.Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString() && s.UserDetail.CreatedBy == createdBy).CountAsync();

                UserDetailsResponseResource response = new UserDetailsResponseResource();
                response.UserDetails = result;
                response.TotalCount = totalCount;

                return response;
            }
        }

        public async Task<UserDetailsResponseResource> SearchApplicationUserByEmail(string email, Core.Enums.Role role, int index, int size)
        {
            if (size <= 0)
            {
                size= 10;
            }
            int takeCount = size;
            int skipCount = index * size;

            var result = await WPContext.Users
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCLevelDetail)
                .Include(s => s.UserDetail)
                    .ThenInclude(s => s.KYCStatus)
                .Include(s => s.UserRoles)
                    .ThenInclude(s => s.Role)
                .Include(s => s.Address)
                .Include(s => s.Wallets)
                    .ThenInclude(s => s.Currency)
                .OrderByDescending(w => w.UserDetail.CreatedDate)
                .Where(entity => entity.Email.Contains(email) && entity.UserRoles.FirstOrDefault().Role.Name == role.ToString())
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();

            var totalCount = await WPContext.Users
                              .Where(entity => entity.Email.Contains(email) && entity.UserRoles.FirstOrDefault().Role.Name == role.ToString()).CountAsync();

            UserDetailsResponseResource response = new UserDetailsResponseResource();
            response.UserDetails = result;
            response.TotalCount = totalCount;

            return response;
        }

        public async Task<TransactionAndUserResponseResource> VerifyUserAccountByPhone(string phoneNumber = "")
        {
            var appUserQuery = WPContext.Users
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
                .Include(s => s.Wallets)
                        .ThenInclude(s => s.WalletType)
                 .AsQueryable();

            appUserQuery = appUserQuery.Where(user => user.UserDetail.PhoneNumber == phoneNumber);
            var UserDetails= await appUserQuery.FirstOrDefaultAsync();

            if(UserDetails == null)
            {
                return null;
            }

            List<IList<Transaction>> transactionResultsList = new List<IList<Transaction>>();

            foreach (var item in UserDetails?.Wallets)
            {
                var transactionQuery =WPContext.Transactions.Where(w => w.FromWalletId == item.Id || w.ToWalletId == item.Id).Include(x=>x.TransferType);
                var transactionsForWallet = transactionQuery.ToList();

                transactionResultsList.Add(transactionsForWallet);
            }

            TransactionAndUserResponseResource transactionAndUserResponse = new TransactionAndUserResponseResource();
            transactionAndUserResponse.UserDetails = UserDetails;
            transactionAndUserResponse.Transactions = transactionResultsList.SelectMany(x => x).ToList(); ;

            return transactionAndUserResponse;
        }

        public async Task<ApplicationUser> GetStaffPermissionsById(string userId)
        {
            var userDetails = await WPContext.Users.Where(user => user.Id == userId)
                            .Include(s => s.UserDetail)
                            .FirstOrDefaultAsync();
            
            return userDetails;
        }
    }
}
