using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

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
            try
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
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public  IEnumerable<CustomerTickets> GetAllCustomerTickets()
        {
            try
            {
                var tickets =  WPContext.CustomerTickets.ToList();

                return tickets;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
    

    
}
