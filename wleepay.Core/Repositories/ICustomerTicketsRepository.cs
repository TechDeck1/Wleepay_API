using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ICustomerTicketsRepository
    {

        Task<CustomerTickets> AddUpdateCustomerTickets(CustomerTickets customerTickets);
        IEnumerable<CustomerTickets> GetAllCustomerTickets();
        IEnumerable<CustomerTickets> GetCustomerTicketsByPhoneNumber(string phone);


    }
}
