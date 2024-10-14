using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Smo;
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
using static DevExpress.XtraPrinting.Native.PageSizeInfo;

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

        public async Task<int> GetTotalSubscribersAsync()
        {
            return await WPContext.Users.Where(s=> s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString()).CountAsync();
        }

        public async Task<int> GetActiveSubscribersAsync()
        {
            return await WPContext.Users.CountAsync(s => s.UserDetail.Status == Status.Active.GetHashCode() && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString());
        }

        public async Task<int> GetInactiveSubscribersAsync(DateTime since)
        {
            return await WPContext.Users.CountAsync(s => s.UserDetail.Status == Status.Active.GetHashCode()&& s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString() && s.UserDetail.CreatedDate < since);
        }

        public async Task<decimal> GetActiveSubscriberLast30DaysAsync()
        {
            var retained = await WPContext.Users.CountAsync(s => s.UserDetail.CreatedDate >= DateTime.Today.AddDays(-30) && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString());
            var activeLast30Days = await WPContext.Transactions
                                    .Where(t => t.CreatedDate.Date >= DateTime.Today.AddDays(-30) && t.CreatedDate.Date <= DateTime.Today && t.FromWallet.ApplicationUser.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString())
                                    .Select(t => t.FromWallet.UserId).Distinct().CountAsync();
            var total = await WPContext.Users.CountAsync();
            return total == 0 ? 0 : (decimal)activeLast30Days / total * 100;
        }

        public async Task<decimal> GetSubscriberRetentionRateAsync()
        {
            var retained = await WPContext.Users.CountAsync(s => s.UserDetail.CreatedDate >= DateTime.Today.AddDays(-30) && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString());
            var activeLast30Days = await WPContext.Transactions
                                    .Where(t => t.CreatedDate.Date >= DateTime.Today.AddDays(-30) && t.CreatedDate.Date <= DateTime.Today && t.FromWallet.ApplicationUser.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString())
                                    .Select(t => t.FromWallet.UserId).Distinct().CountAsync();
            var total = await WPContext.Users.CountAsync();
            return total == 0 ? 0 : (decimal)activeLast30Days / total * 100;
        }

        public async Task<decimal> GetActiveSubscriberTodayAsync()
        {
            var activeToday = await WPContext.Transactions
                            .Where(t => t.CreatedDate == DateTime.Today && t.FromWallet.ApplicationUser.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString())
                            .Select(t => t.FromWallet.UserId).Distinct().CountAsync();

            var total = await WPContext.Users.CountAsync();
            return total == 0 ? 0 : (decimal)activeToday / total * 100;
        }

        public async Task<decimal> GetActiveSubscriberLast7daysAsync()
        {
            var activeLast7Days = await WPContext.Transactions
                            .Where(t => t.CreatedDate.Date >= DateTime.Today.AddDays(-7) && t.CreatedDate.Date <= DateTime.Today && t.FromWallet.ApplicationUser.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString())
                            .Select(t => t.FromWallet.UserId).Distinct().CountAsync();

            var total = await WPContext.Users.CountAsync();
            return total == 0 ? 0 : (decimal)activeLast7Days / total * 100;
        }

        public async Task<decimal> GetSubscriberChurnRateAsync()
        {
            var churned = await WPContext.Users.CountAsync(s => s.UserDetail.CreatedDate < DateTime.Now.AddDays(-30));
            var subscribersWithNoTransactions = await WPContext.Users
                                        .Where(s => !WPContext.Transactions.Any(t => t.FromWallet.UserId == s.Id || t.ToWallet.UserId == s.Id) && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString())
                                        .CountAsync();

            var total = await WPContext.Users.CountAsync();
            var churnRate = (subscribersWithNoTransactions / (double)total) * 100;
            return total == 0 ? 0 : (decimal)subscribersWithNoTransactions / total * 100;
        }

        public async Task<UserDetailsResponseResource> GetReportOfUserByStatusAsync(int role, int status, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var result = await WPContext.Users
                .Where(s => s.UserDetail.Status == status && s.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString())
                .Include(s=>s.UserDetail)
                .Include(s => s.UserRoles)
                       .ThenInclude(s => s.Role)
                .OrderByDescending(w => w.UserDetail.CreatedDate)
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();

            return new UserDetailsResponseResource
            {
                TotalCount = await WPContext.Users.Where(s => s.UserDetail.Status == status && s.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString()).CountAsync(),
                UserDetails = result
            };

        }

        public async Task<UserDetailsResponseResource> GetInActiveSubscriberAndMemberListAsync(int role)
        {
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);

            // Query to get all inactive subscribers for the last 30 days
            var inactiveSubscribers = WPContext.Users
                .Where(subscriber => subscriber.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString() && !WPContext.Transactions
                    .Any(transaction =>
                        (WPContext.Wallet
                            .Any(wallet => wallet.UserId == subscriber.Id &&
                                          (wallet.Id == transaction.FromWalletId || wallet.Id == transaction.ToWalletId))
                         && transaction.CreatedDate >= thirtyDaysAgo))).Include(s=>s.UserDetail)
                .ToList();

            return new UserDetailsResponseResource
            {
                TotalCount = inactiveSubscribers.Count(),
                UserDetails = inactiveSubscribers
            };
        }

        public async Task<UserDetailsResponseResource> GetActiveSubscriberAndMemberListAsync(int role)
        {
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            var activeSubscribers = WPContext.Users
                .Select(subscriber => new
                {
                    Subscriber = subscriber,
                    TransactionCount = WPContext.Transactions
                        .Where(transaction =>
                            WPContext.Wallet
                                .Any(wallet => wallet.UserId == subscriber.Id &&
                                              (wallet.Id == transaction.FromWalletId || wallet.Id == transaction.ToWalletId))
                            && transaction.CreatedDate >= thirtyDaysAgo)
                        .Count()
                })
                .Where(x => x.TransactionCount > 0 && x.Subscriber.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString())  // Only include subscribers with at least 1 transaction in the last 30 days
                .OrderByDescending(x => x.TransactionCount)  // Order by the number of transactions (most active first)
                .Select(x => x.Subscriber).Include(s => s.UserDetail).ToList();

            return new UserDetailsResponseResource
            {
                TotalCount = activeSubscribers.Count(),
                UserDetails = activeSubscribers
            };
        }

        public async Task<UserDetailsResponseResource> GetSubscriberAndMemberListByWalletBalanceAsync(int currencyId, int role, double balance)
        {
            var listByWalletbalance = await WPContext.Users.Where(users => users.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString() && WPContext.Wallet.Any(w => w.UserId == users.Id && w.Balance >= balance && w.CurrencyId == currencyId)).Include(s => s.UserDetail).ToListAsync();
            
            return new UserDetailsResponseResource
            {
                TotalCount = listByWalletbalance.Count(),
                UserDetails = listByWalletbalance
            };
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
