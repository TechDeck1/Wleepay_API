using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Broker;
using SendGrid.Helpers.Mail;
using Stripe;
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
using wleepay.Core.Services;
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
            var userDetails = await WPContext.UserDetail.Where(user => user.UserId == userId)
                .Include(s => s.KYCLevelDetail)
                .Include(s => s.KYCStatus)
                .Include(s => s.County)
                .Include(s => s.ApplicationUser.UsersStaffRole)
                    .ThenInclude(s => s.StaffRoles)
                .Include(s => s.ApplicationUser.UserRoles)
                    .ThenInclude(s => s.Role)
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
                .Where(user => user.UserRoles != null && user.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString() && user.UserDetail.Status == Status.Active.GetHashCode())
                .ToListAsync();
            return result;
        }

        public async Task<UserDetailsResponseResource> GetUsers(Core.Enums.Role role, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var result = await WPContext.Users
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
                .Include(s => s.UsersStaffRole)
                    .ThenInclude(s => s.StaffRoles)
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
        public async Task<WleeSendUserDetails> GetWleeSendUserDetailsByPhone(string phone)
        {
            return await WPContext.WleeSendUserDetail.Where(user => user.PhoneNumber == phone)
                .Include(s => s.County)
                .Include(s => s.WleeSendUser)
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

        public async Task<UserDetailsResponseResource> GetAllUsersById(Core.Enums.Role role, int pageIndex = 0, int pageSize = 0, string createdBy = "")
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
                return new UserDetailsResponseResource
                {
                    UserDetails = result,
                    TotalCount = await WPContext.Users.Where(s => s.UserRoles.FirstOrDefault().Role.Name == role.ToString() && s.UserDetail.CreatedBy == createdBy).CountAsync(),
                };


            }
        }

        public async Task<UserDetailsResponseResource> SearchApplicationUserByEmail(string searchText, Core.Enums.Role role, int index, int size, int filterType)
        {
            if (size <= 0)
            {
                size = 10;
            }
            int takeCount = size;
            int skipCount = index * size;

            var result = WPContext.Users
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
                .Where(entity => entity.UserRoles.FirstOrDefault().Role.Name == role.ToString());

            if ((FilterUserByType)filterType == FilterUserByType.PhoneNumber)
            {
                result = result.Where(entity => entity.UserDetail.PhoneNumber.Contains(searchText));
            }
            else if ((FilterUserByType)filterType == FilterUserByType.Name)
            {
                result = result.Where(entity => entity.Email.Contains(searchText)
                || entity.UserDetail.FirstName.Contains(searchText)
                || entity.UserDetail.LastName.Contains(searchText));
            }
            else
            {
                result = result.Where(entity =>
                entity.UserDetail.FirstName.Contains(searchText)
                || entity.UserDetail.LastName.Contains(searchText)
                );
            }

            var searchUsers = await result.Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();

            return new UserDetailsResponseResource
            {
                UserDetails = searchUsers,
                TotalCount = await result.CountAsync()
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

            return new TransactionAndUserResponseResource
            {
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
            if (userId == null) { throw new ArgumentNullException("userId"); }
            var userDetails = await WPContext.UserDetail.Where(user => user.UserId == userId).FirstOrDefaultAsync();
            //userDetails.DeviceId = deviceId;
            //userDetails.FcmToken = token;
            await WPContext.SaveChangesAsync();
            return userDetails;

        }

        public async Task<AgentSubscribersCountResponseModel> GetActiveAgentsAndSubscribersCount()
        {
            var agentCount = await WPContext.Users.Include(x => x.UserDetail).Include(r => r.UserRoles).Where(s => s.UserRoles.FirstOrDefault().Role.Name == Role.Agent.ToDescriptionString() && s.UserDetail.Status == Status.Active.GetHashCode()).CountAsync();
            var subscriberCount = await WPContext.Users.Include(x => x.UserDetail).Include(r => r.UserRoles).Where(s => s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString() && s.UserDetail.Status == Status.Active.GetHashCode()).CountAsync();

            return new AgentSubscribersCountResponseModel { AgentCount = agentCount, SubscribersCount = subscriberCount };

        }

        public async Task<decimal> GetEarnedCommission(string transactionType)
        {
            return await WPContext.SochitelTransactions
   .Where(s => s.TransactionType == transactionType)
    .SumAsync(s => Convert.ToDecimal(s.CommissionRate));
        }

        public async Task<IList<CountByMonths>> GetActiveAgentsByMonth()
        {
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);
            var activeAgentsByMonth = WPContext.Users.Include(u => u.UserDetail).Include(r => r.UserRoles)
                .Where(s => s.UserDetail.Status == Status.Active.GetHashCode() && s.UserDetail.CreatedDate >= sixMonthsAgo && s.UserRoles.FirstOrDefault().Role.Name == Role.Agent.ToDescriptionString()).ToList();

            var x = activeAgentsByMonth.GroupBy(s => new { Year = s.UserDetail.CreatedDate.Year, Month = s.UserDetail.CreatedDate.Month })
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

        public async Task<IList<CountByMonths>> GetPendingKYCSubscribersByMonth()
        {
            var activeSubscribersByMonth = WPContext.Users.Include(u => u.UserDetail).Include(r => r.UserRoles)
                .Where(s => s.UserDetail.KYCLevel == 2 && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString()).ToList();


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

        public async Task<IList<CountByMonths>> GetSubscribersSignup(DateTime daysCount)
        {
            //var sixMonthsAgo = DateTime.Now.AddMonths(-6);
            var activeSubscribersByMonth = WPContext.Users.Include(u => u.UserDetail).Include(r => r.UserRoles)
                .Where(s => s.UserDetail.CreatedDate >= daysCount && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString()).ToList();


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
                    Name = "Level" + " " + g.Key.ToString(),
                    Value = g.Count()
                })
                .OrderBy(g => g.Name)
                .ToList();

            return x;
        }

        public async Task<int> GetTotalSubscribersAsync()
        {
            return await WPContext.Users.Where(s => s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString()).CountAsync();
        }

        public async Task<double> GetTotalWalletBalanceByCurrency(int walletType, int currencyId)
        {
            return await WPContext.Wallet
    .Where(s => s.Type == walletType && s.CurrencyId == currencyId)
    .SumAsync(s => s.Balance);
        }

        public async Task<int> GetActiveSubscribersAsync()
        {
            return await WPContext.Users.CountAsync(s => s.UserDetail.Status == Status.Active.GetHashCode() && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString());
        }

        public async Task<int> GetInactiveSubscribersAsync(DateTime since)
        {
            return await WPContext.Users.CountAsync(s => s.UserDetail.Status == Status.Active.GetHashCode() && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString() && s.UserDetail.CreatedDate < since);
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
            //var retained = await WPContext.Users.CountAsync(s => s.UserDetail.CreatedDate >= DateTime.Today.AddDays(-30) && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString());
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
            //var churned = await WPContext.Users.CountAsync(s => s.UserDetail.CreatedDate < DateTime.Now.AddDays(-30));
            var subscribersWithNoTransactions =
                await WPContext.Users.Where(s => !WPContext.Transactions.Any(t => t.FromWallet.UserId == s.Id && t.CreatedDate < DateTime.Now.AddDays(-30))
                && s.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString()).Distinct().CountAsync();

            var total = await WPContext.Users.CountAsync();
            //var churnRate = (subscribersWithNoTransactions / (double)total) * 100;
            return total == 0 ? 0 : (decimal)subscribersWithNoTransactions / total * 100;
        }

        public async Task<UserDetailsResponseResource> GetReportOfUserByStatusAsync(int role, int status, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var result = await WPContext.Users
                .Where(s => s.UserDetail.Status == status && s.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString())
                .Include(s => s.UserDetail)
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

        public async Task<TicketsResponse> GetReportOfUserByOpenTicketsAsync(int role, int status, int pageIndex = 0, int pageSize = 10)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var usersWithTickets = await WPContext.CustomerTickets
    .GroupBy(ticket => ticket.UserId)
    .Where(group => group.Count() > 5)  // Only include groups with more than 5 tickets
    .Select(group => new
    {
        UserId = group.Key,
        TicketsCount = group.Count()  // Get the number of tickets per user
    })
    .Join(WPContext.Users,  // Assuming Users is the DbSet for the User table
          ticketGroup => ticketGroup.UserId,
          user => user.Id,
          (ticketGroup, user) => new
          {
              UserDetails = new
              {
                  user.Id,
                  user.UserDetail.FirstName,
                  user.UserDetail.LastName,
                  user.Email,
                  user.UserDetail.PhoneNumber,
                  user.UserDetail.AccountNumber,
                  user.UserDetail.CreatedDate,
                  TicketsCount = ticketGroup.TicketsCount  // Include the ticket count
              }
          })
    .Skip(skipCount)
    .Take(takeCount)
    .ToListAsync();

            // Return the results
            return new TicketsResponse
            {
                TotalCount = usersWithTickets.Count(),  // Total number of users matching the filter
                UserDetails = usersWithTickets.Select(x => new UserDetails
                {
                    Id = x.UserDetails.Id,
                    FirstName = x.UserDetails.FirstName,
                    LastName = x.UserDetails.LastName,
                    Email = x.UserDetails.Email,
                    PhoneNumber = x.UserDetails.PhoneNumber,
                    AccountNumber = x.UserDetails.AccountNumber,
                    CreatedDate = x.UserDetails.CreatedDate
                }).ToList()
            };

        }

        public async Task<UserDetailsResponseResource> GetInActiveSubscriberAndMemberListAsync(int role)
        {
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);

            // Query to get all inactive subscribers and their last transaction date
            var inactiveSubscribersWithDetails = WPContext.Users
                .Where(subscriber =>
                    subscriber.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString() &&
                    !WPContext.Transactions.Any(transaction =>
                        WPContext.Wallet.Any(wallet =>
                            wallet.UserId == subscriber.Id &&
                            (wallet.Id == transaction.FromWalletId || wallet.Id == transaction.ToWalletId)) &&
                        transaction.CreatedDate >= thirtyDaysAgo))
                .Include(s => s.UserDetail)
                .Select(subscriber => new
                {
                    Subscriber = subscriber,
                    LastTransactionDate = WPContext.Transactions
                        .Where(transaction =>
                            WPContext.Wallet.Any(wallet =>
                                wallet.UserId == subscriber.Id &&
                                (wallet.Id == transaction.FromWalletId || wallet.Id == transaction.ToWalletId)))
                        .OrderByDescending(t => t.CreatedDate)
                        .Select(t => t.CreatedDate)
                        .FirstOrDefault(),
                })
                .ToList();

            // Map the result to the final response
            var userDetails = inactiveSubscribersWithDetails
                .Select(x =>
                {
                    if (x.Subscriber.UserDetail != null)
                    {
                        x.Subscriber.UserDetail.LastTransactionDate = x.LastTransactionDate;
                    }
                    return x.Subscriber;
                })
                .ToList();

            return new UserDetailsResponseResource
            {
                TotalCount = userDetails.Count(),
                UserDetails = userDetails
            };
        }


        public async Task<UserDetailsResponseResource> GetActiveSubscriberAndMemberListAsync(int role)
        {
            DateTime thirtyDaysAgo = DateTime.Now.AddDays(-30);

            // Fetch active subscribers with their transaction details
            var activeSubscribersWithDetails = WPContext.Users
                .Where(subscriber => subscriber.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString())
                .Include(w => w.Wallets)
                .Select(subscriber => new
                {
                    Subscriber = subscriber,
                    TransactionCount = WPContext.Transactions
                        .Where(transaction =>
                            WPContext.Wallet.Any(wallet =>
                                wallet.UserId == subscriber.Id &&
                                (wallet.Id == transaction.FromWalletId || wallet.Id == transaction.ToWalletId)) &&
                            transaction.CreatedDate >= thirtyDaysAgo)
                        .Count(),
                    LastTransactionDate = WPContext.Transactions
                        .Where(transaction =>
                            WPContext.Wallet.Any(wallet =>
                                wallet.UserId == subscriber.Id &&
                                (wallet.Id == transaction.FromWalletId || wallet.Id == transaction.ToWalletId)))
                        .OrderByDescending(t => t.CreatedDate)
                        .Select(t => t.CreatedDate)
                        .FirstOrDefault(),
                    UserDetail = subscriber.UserDetail // Explicitly include UserDetail
                })
                .Where(x => x.TransactionCount > 0) // Only include subscribers with transactions
                .OrderByDescending(x => x.TransactionCount) // Order by transaction count
                .ToList();

            // Map the result to the final response
            var userDetails = activeSubscribersWithDetails
                .Select(x =>
                {
                    if (x.Subscriber.UserDetail != null)
                    {
                        x.Subscriber.UserDetail.LastTransactionDate = x.LastTransactionDate;
                    }
                    return x.Subscriber;
                })
                .ToList();

            return new UserDetailsResponseResource
            {
                TotalCount = userDetails.Count(),
                UserDetails = userDetails
            };
        }



        public async Task<UserDetailsResponseResource> GetSubscriberAndMemberListByWalletBalanceAsync(int currencyId, int role, double balance, int walletType)
        {
            var listByWalletbalance = await WPContext.Users.Where(users => users.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString() && WPContext.Wallet.Any(w => w.UserId == users.Id && w.Balance >= balance && w.CurrencyId == currencyId && w.Type == walletType)).Include(s => s.UserDetail).Include(w => w.Wallets).ToListAsync();

            return new UserDetailsResponseResource
            {
                TotalCount = listByWalletbalance.Count(),
                UserDetails = listByWalletbalance
            };
        }

        public async Task<UserDetailsResponseResource> GetReportBySubscribersKYCLevelAsync(int level, int role)
        {
            var listByKYCLevel = await WPContext.Users.Where(users => users.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString() && users.UserDetail.KYCLevel == level).Include(s => s.UserDetail).ToListAsync();

            return new UserDetailsResponseResource
            {
                TotalCount = listByKYCLevel.Count(),
                UserDetails = listByKYCLevel
            };
        }

        public async Task<UserDetailsResponseResource> GetAllActiveAccountsAsync(int role)
        {
            var listByKYCLevel = await WPContext.Users.Where(users => users.UserRoles.FirstOrDefault().Role.Name == ((Core.Enums.Role)role).ToString() && users.UserDetail.Status == Status.Active.GetHashCode()).Include(s => s.UserDetail).ToListAsync();

            return new UserDetailsResponseResource
            {
                TotalCount = listByKYCLevel.Count(),
                UserDetails = listByKYCLevel
            };
        }
        public async Task<UserDetailsResponseResource> GetAccountOpenedByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            var listByDateRange = await WPContext.Users
                .Where(users => users.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString() && users.UserDetail.CreatedDate > fromDate && users.UserDetail.CreatedDate < toDate).Include(w => w.Wallets).Include(s => s.UserDetail).OrderByDescending(x => x.UserDetail.CreatedDate).ToListAsync();

            return new UserDetailsResponseResource
            {
                TotalCount = listByDateRange.Count(),
                UserDetails = listByDateRange
            };
        }

        public async Task<int> GetSubscribersCountByCountryAsync(int countryId)
        {
            var subscriberCountbyCountry = await WPContext.Users.Where(users => users.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString() && users.UserDetail.CountyId == countryId).Include(s => s.UserDetail).CountAsync();

            return subscriberCountbyCountry;
        }

        public async Task<int> GetSubscribersCountByAgeAsync(int minAge, int maxAge)
        {
            DateTime today = DateTime.Today;
            var subscriberCountByAgeRange = 0;
            DateTime min = today.AddYears(-minAge);
            if (maxAge != 0)
            {
                DateTime max = today.AddYears(-maxAge);
                subscriberCountByAgeRange = await WPContext.Users
                   .Include(s => s.UserDetail)
                   .Where(users => users.UserDetail.DOB <= min && users.UserDetail.DOB >= max && users.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString())
                   .CountAsync();
            }
            else
            {
                subscriberCountByAgeRange = await WPContext.Users
                   .Include(s => s.UserDetail)
                   .Where(users => users.UserDetail.DOB <= min && users.UserRoles.FirstOrDefault().Role.Name == Role.Member.ToDescriptionString())
                   .CountAsync();
            }


            return subscriberCountByAgeRange;
        }

        public async Task<PayBillCreditPurchaseCount> GetBillPayCreditPurchaseCount()
        {
            var lECPurchaseCount = await WPContext.SochitelTransactions.Where(t => t.CreatedDate >= DateTime.Today.AddDays(-7) && t.CreatedDate <= DateTime.Today && t.TransactionType == "Bill Payment").CountAsync();
            var mobileDataPurchaseCount = await WPContext.SochitelTransactions.Where(t => t.CreatedDate >= DateTime.Today.AddDays(-7) && t.CreatedDate <= DateTime.Today && t.TransactionType == "Mobile Recharge").CountAsync();

            return new PayBillCreditPurchaseCount
            {
                LECPurchaseCount = lECPurchaseCount,
                MobileDataPurchaseCount = mobileDataPurchaseCount
            };
        }

        public async Task<UserNotes> AddUserNotes(string userId, string addBy, string description, string note, Core.Models.MessageType messageType)
        {
            var userNote = new UserNotes
            {
                Note = note,
                userId = userId,
                CreatedDate = DateTime.Now,
                UpdatedDate = null,
                AddedBy = addBy,
                IsDeleted = false,
                MessageType = messageType
            };

            await WPContext.UserNotes.AddAsync(userNote);

            await WPContext.SaveChangesAsync();
            return userNote;
        }


        public async Task<UserNotesReponse> GetUserNotes(string userId, int messageType)
        {
            var userNotes = WPContext.UserNotes
                .Include(u => u.ApplicationUser.UserDetail)
                .Where(n => n.userId == userId && n.MessageType == (Core.Models.MessageType)messageType)
                .ToList();

            // Use the UserId property of addedBy to get the string IDs

            return new UserNotesReponse
            {
                TotalCount = userNotes.Count(),
                UserNotes = userNotes
            };
        }

    }
}
