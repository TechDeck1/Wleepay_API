using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Common;
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
                .Include(s=> s.ApplicationUser.UserRoles)
                    .ThenInclude(s=>s.Role)
                .FirstOrDefaultAsync();
            return userDetails;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersByRole(int role)
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

            return new UserDetailsResponseResource
            {
                TotalCount = await WPContext.Users.Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString()).CountAsync(),
                UserDetails = result
            };
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
                .Include(s => s.Address)
                .Include(s => s.Wallets)
                    .ThenInclude(s => s.Currency)
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
                    .Include(s => s.UsersStaffRole)
                        .ThenInclude(s => s.StaffRoles)
                    .Include(s => s.Address)
                    .OrderByDescending(x => x.UserDetail.CreatedDate)
                    .Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString() && s.UserDetail.CreatedBy == createdBy)
                    .Skip(skipCount)
                    .Take(takeCount)
                    .ToListAsync();

                return new UserDetailsResponseResource
                {
                    TotalCount = await WPContext.Users.Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString() && s.UserDetail.CreatedBy == createdBy).CountAsync(),
                    UserDetails = result
                };
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
                 .Include(s => s.UsersStaffRole)
                        .ThenInclude(s => s.StaffRoles)
                 .Include(s => s.Address)
                 .OrderByDescending(x => x.UserDetail.CreatedDate)
                 .Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString() && s.UserDetail.CreatedBy == createdBy)
                 .ToListAsync();

               // var totalCount = await WPContext.Users.Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString() && s.UserDetail.CreatedBy == createdBy).CountAsync();
                return new UserDetailsResponseResource{
                   UserDetails = result,
                   TotalCount = await WPContext.Users.Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString() && s.UserDetail.CreatedBy == createdBy).CountAsync(),
                };
              

            }
        }

        public async Task<UserDetailsResponseResource> SearchApplicationUserByEmail(string searchText, Core.Enums.Role role, int index, int size)
        {
            if (size <= 0)
            {
                size = 10;
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
                .Where(entity => (entity.Email.Contains(searchText) || entity.UserDetail.PhoneNumber.Contains(searchText) || entity.UserDetail.FirstName.Contains(searchText) || entity.UserDetail.LastName.Contains(searchText)) && entity.UserRoles.FirstOrDefault().Role.Name == role.ToString())
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();

            var totalCount = await WPContext.Users
                              .Where(entity => entity.Email.Contains(searchText) && entity.UserRoles.FirstOrDefault().Role.Name == role.ToString()).CountAsync();

            return new UserDetailsResponseResource
            {
                UserDetails = result,
                TotalCount = await WPContext.Users
                            .Where(entity => entity.Email.Contains(searchText) && entity.UserRoles.FirstOrDefault().Role.Name == role.ToString()).CountAsync()
            };
            
            //UserDetailsResponseResource response = new UserDetailsResponseResource();
            //response.UserDetails = result;
            //response.TotalCount = totalCount;

            //return response;

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
            var UserDetails = await appUserQuery.FirstOrDefaultAsync();

            if (UserDetails == null)
            {
                return null;
            }

            List<IList<Transaction>> transactionResultsList = new List<IList<Transaction>>();

            foreach (var item in UserDetails?.Wallets)
            {
                var transactionQuery = WPContext.Transactions.Where(w => w.FromWalletId == item.Id || w.ToWalletId == item.Id).Include(x => x.TransferType);
                var transactionsForWallet = transactionQuery.ToList();

                transactionResultsList.Add(transactionsForWallet);
            }

            return new TransactionAndUserResponseResource {
                UserDetails = UserDetails,
                Transactions = transactionResultsList.SelectMany(x => x).ToList()
            };
        }

        public async Task<ApplicationUser> GetStaffPermissionsById(string userId)
        {
            var permissions = await WPContext.Users.Where(user => user.Id == userId)
                            .Include(s => s.UserDetail)
                            .FirstOrDefaultAsync();
            return permissions;
        }

        public async Task<UserDetail> AddDeviceIdAndToken(string deviceId, string token, string userId)
        {
            if(userId == null) { throw new ArgumentNullException("userId"); }
            var userDetails = await WPContext.UserDetail.Where(user => user.UserId == userId).FirstOrDefaultAsync();
            //userDetails.DeviceId = deviceId;
            //userDetails.FcmToken = token;
            await WPContext.SaveChangesAsync();
            return userDetails;

        }

        public async Task<AgentSubscribersCountResponseModel> GetActiveAgentsAndSubscribersCount()
        {
            var agentCount = await WPContext.Users.Include(x => x.UserDetail).Include(r=>r.UserRoles).Where(s => s.UserRoles.FirstOrDefault().Role.Name == Role.Agent.ToDescriptionString() && s.UserDetail.Status == Status.Active.GetHashCode()).CountAsync();
            var subscriberCount = await WPContext.Users.Include(x => x.UserDetail).Include(r => r.UserRoles).Where(s => s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString() && s.UserDetail.Status == Status.Active.GetHashCode()).CountAsync();

            return new AgentSubscribersCountResponseModel { AgentCount = agentCount, SubscribersCount = subscriberCount };

        }

        public async Task<IList<CountByMonths>> GetActiveAgentsByMonth()
        {
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);
            var activeAgentsByMonth = WPContext.Users.Include(u => u.UserDetail).Include(r => r.UserRoles)
                .Where(s => s.UserDetail.Status == Status.Active.GetHashCode() && s.UserDetail.CreatedDate >= sixMonthsAgo && s.UserRoles.FirstOrDefault().Role.Name == Role.Agent.ToDescriptionString()).ToList();

            var x = activeAgentsByMonth.GroupBy(s => new  { Year = s.UserDetail.CreatedDate.Year, Month = s.UserDetail.CreatedDate.Month })
                .Select(g => new CountByMonths
                {
                    Name = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy"), // Format after grouping
                    Value = g.Count()
                })
                .OrderByDescending(g => g.Name)
                .ToList();

            return x;
        }

        public async Task<IList<CountByMonths>> GetActiveSubscribersByMonth()
        {
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);
            var activeSubscribersByMonth = WPContext.Users.Include(u => u.UserDetail).Include(r => r.UserRoles)
                .Where(s => s.UserDetail.Status == Status.Active.GetHashCode() && s.UserDetail.CreatedDate >= sixMonthsAgo && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString()).ToList();


            var x = activeSubscribersByMonth.GroupBy(s => new { year = s.UserDetail.CreatedDate.Year, month = s.UserDetail.CreatedDate.Month })
                .Select(g => new CountByMonths
                {
                    Name = new DateTime(g.Key.year, g.Key.month, 1).ToString("MMMM yyyy"), // Format after grouping
                    Value = g.Count()
                })
                .OrderBy(g => g.Name)
                .ToList();

            return x;
        }
       
        public async Task<IList<subscribersByType>> GetActiveSubscribersByType()
        {
            var activeSubscribersByMonth = WPContext.Users.Include(u => u.UserDetail).Include(r => r.UserRoles)
                .Where(s => s.UserDetail.Status == Status.Active.GetHashCode() && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString()).ToList();


            var x = activeSubscribersByMonth
                .GroupBy(t => t.UserDetail.KYCLevel)  // Grouping by transaction type
                .Select(g => new subscribersByType
                {
                    Name = "Level"+" "+g.Key.ToString(),
                    Value = g.Count()
                })
                .OrderBy(g => g.Name)
                .ToList();

            return x;
        }

        public async Task<subscribersByType> GetOverAllSubscriberData()
        {

            subscribersByType subscribersByType = new subscribersByType();
            var today = DateTime.Today;
            var sevenDaysAgo = today.AddDays(-7);
            var thirtyDaysAgo = today.AddDays(-30);

            var totalSubscribers = await WPContext.Users.CountAsync();
            var retainedSubscribers = await WPContext.Transactions.Select(t => t.FromWallet.UserId).Distinct().CountAsync();
            var retentionRate = (retainedSubscribers / (double)totalSubscribers) * 100;

            var activeSubscribersL1 = await WPContext.Users.Include(s=>s.UserDetail)
                .Where(s => s.UserDetail.KYCLevel == wleepay.Core.Enums.KYCLevel.L1.GetHashCode() || s.Wallets.FirstOrDefault().TransactionFrom.Any()|| s.Wallets.FirstOrDefault().TransactionTo.Any())
                .CountAsync();

            var activeSubscribersL2 = await WPContext.Users
                .Where(s => s.UserDetail.KYCLevel == wleepay.Core.Enums.KYCLevel.L2.GetHashCode() || s.Wallets.FirstOrDefault().TransactionFrom.Any() || s.Wallets.FirstOrDefault().TransactionTo.Any())
                .CountAsync();

            var activeSubscribersL3 = await WPContext.Users
                .Where(s => s.UserDetail.KYCLevel == wleepay.Core.Enums.KYCLevel.L3.GetHashCode() || s.Wallets.FirstOrDefault().TransactionFrom.Any() || s.Wallets.FirstOrDefault().TransactionTo.Any())
                .CountAsync();
           

            var activeToday = await WPContext.Transactions
                .Where(t => t.CreatedDate == today)
                .Select(t => t.FromWallet.UserId).Distinct().CountAsync();

            var activeLast7Days = await WPContext.Transactions
                .Where(t => t.CreatedDate.Date >= sevenDaysAgo && t.CreatedDate.Date <= today)
                .Select(t => t.FromWallet.UserId).Distinct().CountAsync();

            var activeLast30Days = await WPContext.Transactions
                .Where(t => t.CreatedDate.Date >= thirtyDaysAgo && t.CreatedDate.Date <= today)
                .Select(t => t.FromWallet.UserId).Distinct().CountAsync();

            var subscribersWithNoTransactions = await WPContext.Users
                .Where(s => !WPContext.Transactions.Any(t => t.FromWallet.UserId == s.Id || t.ToWallet.UserId == s.Id))
                .CountAsync();

            var churnRate = (subscribersWithNoTransactions / (double)totalSubscribers) * 100;

            var inactiveSubscribersLast30Days = await WPContext.Users
                .Where(s => !WPContext.Transactions
                    .Any(t => (t.FromWallet.UserId == s.Id || t.ToWallet.UserId == s.Id) && t.CreatedDate.Date >= thirtyDaysAgo))
                .CountAsync();

            var previousThirtyDaysAgo = today.AddDays(-60);

            var inactiveSubscribersPrevious30Days = await WPContext.Users
                .Where(s => !WPContext.Transactions
                    .Any(t =>(t.FromWallet.UserId == s.Id || t.ToWallet.UserId == s.Id) && t.CreatedDate.Date >= previousThirtyDaysAgo && t.CreatedDate.Date < thirtyDaysAgo))
                .CountAsync();

            var activeTodayPercentage = (activeToday / (double)totalSubscribers) * 100;
            var activeLast7DaysPercentage = (activeLast7Days / (double)totalSubscribers) * 100;
            var inactiveLast30DaysPercentage = (inactiveSubscribersLast30Days / (double)totalSubscribers) * 100;

            return subscribersByType;

      }

    }
}
