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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.VisualBasic;
using wleepay.Core.DTO.Response;
using Microsoft.SqlServer.Management.Smo;

namespace wleepay.Data.Repositories
{
    public class CustomerTicketsRepository : Repository<CustomerTickets>, ICustomerTicketsRepository
    {

        public CustomerTicketsRepository(WPContext context)
           : base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<CustomerTickets> AddUpdateCustomerTickets(CustomerTickets customerTickets)
        {
            if (customerTickets.Id <= 0)
            {
                await WPContext.CustomerTickets.AddAsync(customerTickets);

            }
            else
            {
                var tickets = await WPContext.CustomerTickets.Where(x => x.Id == customerTickets.Id)
                                                             .Include(x => x.Notes)
                                                             .FirstOrDefaultAsync();
                tickets.UpdatedDate = DateTime.UtcNow;
                tickets.NotificationStatus = customerTickets.NotificationStatus;
            }
            await WPContext.SaveChangesAsync();
            return customerTickets;

        }

        public async Task<CustomerServiceResponseResource> GetAllCustomerTickets(string phoneNumber, int status, int pageIndex, int? pageSize)
        {
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            int takeCount = (int)pageSize;
            int skipCount = (int)(pageIndex * pageSize);
            var totalCount = 0;
            List<CustomerTickets> tickets;
            if (string.IsNullOrEmpty(phoneNumber) && status == (int)CustomerTicketStatus.Closed)
            {
                tickets = await WPContext.CustomerTickets.Include(x => x.Notes.OrderByDescending(x => x.CreatedDate))
                                                           .Include(x => x.IssueType)
                                                           .OrderByDescending(x => x.CreatedDate)
                                                           .Skip(skipCount)
                                                           .Take(takeCount)
                                                           .ToListAsync();
                totalCount = await WPContext.CustomerTickets.CountAsync();

            }
            else if (string.IsNullOrEmpty(phoneNumber) && status == (int)CustomerTicketStatus.Open)
            {
                tickets = await WPContext.CustomerTickets.Where(x => x.Status == (int)CustomerTicketStatus.Open || x.Status == (int)CustomerTicketStatus.Updated)
                                                           .Include(x => x.Notes.OrderByDescending(x => x.CreatedDate))
                                                           .Include(x => x.IssueType)
                                                           .OrderByDescending(x => x.CreatedDate)
                                                           .Skip(skipCount)
                                                           .Take(takeCount)
                                                           .ToListAsync();
                totalCount = await WPContext.CustomerTickets.Where(x => x.Status == (int)CustomerTicketStatus.Open || x.Status == (int)CustomerTicketStatus.Updated).CountAsync();

            }
            else if (!string.IsNullOrEmpty(phoneNumber) && status == (int)CustomerTicketStatus.Closed)
            {
                tickets = await WPContext.CustomerTickets.Where(x => x.PhoneNumber.Contains(phoneNumber))
                                                             .Include(x => x.Notes.OrderByDescending(x => x.CreatedDate))
                                                             .Include(x => x.IssueType)
                                                             .OrderByDescending(x => x.CreatedDate)
                                                             .Skip(skipCount)
                                                             .Take(takeCount)
                                                             .ToListAsync();
                totalCount = await WPContext.CustomerTickets.Where(x => x.Status == (int)CustomerTicketStatus.Open && x.PhoneNumber.Contains(phoneNumber) || x.Status == (int)CustomerTicketStatus.Updated).CountAsync();

            }
            else
            {
                tickets = await WPContext.CustomerTickets.Where(x => x.Status == (int)CustomerTicketStatus.Open && x.PhoneNumber.Contains(phoneNumber) || x.Status == (int)CustomerTicketStatus.Updated)
                                                           .Include(x => x.Notes.OrderByDescending(x => x.CreatedDate))
                                                           .Include(x => x.IssueType)
                                                           .OrderByDescending(x => x.CreatedDate)
                                                           .Skip(skipCount)
                                                           .Take(takeCount)
                                                           .ToListAsync();
                totalCount = await WPContext.CustomerTickets.Where(x => x.Status == (int)CustomerTicketStatus.Open && x.PhoneNumber.Contains(phoneNumber) || x.Status == (int)CustomerTicketStatus.Updated).CountAsync();

            }

            return new CustomerServiceResponseResource
            {
                TotalCount = totalCount,
                CustomerTickets = tickets
            };
        }

        public async Task<CustomerServiceResponseResource> GetCustomerTicketsByPhoneNumber(string phone, int pageIndex, int pageSize)
        {
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;

            var tickets = await WPContext.CustomerTickets.Where(x => x.PhoneNumber == phone)
                                                    .Include(x => x.Notes)
                                                    .Include(x => x.IssueType)
                                                    .OrderByDescending(x => x.CreatedDate)
                                                    .Skip(skipCount)
                                                    .Take(takeCount)
                                                    .ToListAsync();

            return new CustomerServiceResponseResource 
            { 
                TotalCount = await WPContext.CustomerTickets.Where(x => x.PhoneNumber == phone).CountAsync(),
                CustomerTickets = tickets,
             };
        }

        public async Task<CustomerTickets> GetCustomerTicketsById(int Id)
        {
            return await WPContext.CustomerTickets.Where(x => x.Id == Id)
                                                    .Include(x => x.Notes.OrderByDescending(x => x.CreatedDate))
                                                    .Include(x => x.IssueType)
                                                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomerTickets>> GetCustomerTicketsByUserId(string userId)
        {
            return await WPContext.CustomerTickets.Where(x => x.UserId == userId)
                                                    .Include(x => x.Notes.OrderByDescending(x => x.CreatedDate))
                                                    .Include(x => x.IssueType)
                                                    .ToListAsync();
        }

        public async Task<IEnumerable<CustomerTickets>> GetAllTickets()
        {

            var tickets = await WPContext.CustomerTickets
                                                 .Include(x => x.Notes.OrderByDescending(x => x.CreatedDate))
                                                .Include(x => x.IssueType).OrderByDescending(x => x.CreatedDate).ToListAsync();
            return tickets;
        }
    }

}
