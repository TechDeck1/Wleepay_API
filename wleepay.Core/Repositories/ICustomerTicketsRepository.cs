using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ICustomerTicketsRepository
    {

        Task<CustomerTickets> AddUpdateCustomerTickets(CustomerTickets customerTickets);
        Task<CustomerServiceResponseResource> GetAllCustomerTickets(string phoneNumber,int status, int pageIndex, int? pageSize);
        Task<CustomerServiceResponseResource> GetCustomerTicketsByPhoneNumber(string phone, int pageIndex, int pageSize);
        Task<CustomerTickets> GetCustomerTicketsById(int Id);



    }
}
