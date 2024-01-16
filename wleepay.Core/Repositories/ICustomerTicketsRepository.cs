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
        Task<IEnumerable<CustomerTickets>> GetAllCustomerTickets();
        Task<IEnumerable<CustomerTickets>> GetCustomerTicketsByPhoneNumber(string phone);
        Task<CustomerTickets> GetCustomerTicketsById(int Id);



    }
}
