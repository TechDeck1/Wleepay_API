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
            var tickets = await WPContext.CustomerTickets.AddAsync(customerTickets);

            //if (customerTickets.Notes != null)
            //{
            //    foreach (var note in customerTickets.Notes)
            //    {
            //        Notes CustomerNotes = new Notes();
            //        CustomerNotes.Note = note.ToString();
            //        CustomerNotes.CreatedDate = DateTime.UtcNow;
            //        CustomerNotes.CustomerTicketId = customerTickets.Id;
            //        await WPContext.Notes.AddAsync(CustomerNotes);

            //    }
            //}

            WPContext.SaveChanges();
            return customerTickets;

        }


        public async Task<IEnumerable<CustomerTickets>> GetAllCustomerTickets()
        {
             var tickets = await  WPContext.CustomerTickets.Include(x=>x.Notes).OrderByDescending(x=>x.CreatedDate).ToListAsync();
             return tickets;

        }

        public async Task<IEnumerable<CustomerTickets>> GetCustomerTicketsByPhoneNumber(string phone)
        {
            // Assuming WPContext is a properly configured and disposed database context
            var tickets = await WPContext.CustomerTickets.Where(x => x.PhoneNumber == phone)
                                                    .Include(x => x.Notes)
                                                    .OrderByDescending(x => x.CreatedDate)
                                                    .ToListAsync();
            return tickets;
        }


    }
    

    
}
