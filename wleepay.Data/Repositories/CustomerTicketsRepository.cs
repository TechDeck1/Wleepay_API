using System;
using System.Collections.Generic;
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

        public Task<CustomerTickets> AddUpdateCustomerTickets(CustomerTickets customerTickets)
        {
            throw new NotImplementedException();
        }
    }
    

    
}
