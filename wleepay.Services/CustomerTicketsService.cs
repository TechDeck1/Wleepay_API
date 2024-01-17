using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;
using static DevExpress.XtraPrinting.Native.PageSizeInfo;

namespace wleepay.Services
{
    public class CustomerTicketsService : ICustomerTicketsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerTicketsService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<CustomerTickets> AddUpdateCustomerTickets(CustomerTickets customerTickets)
        {
            var tickets = await _unitOfWork.CustomerTickets.AddUpdateCustomerTickets(customerTickets);
            return tickets;
        }

        public async Task<CustomerServiceResponseResource> GetAllCustomerTickets(int pageIndex, int pageSize)
        {
            var tickets = await _unitOfWork.CustomerTickets.GetAllCustomerTickets(pageIndex , pageSize);
            return tickets;
        }

        public async Task<CustomerServiceResponseResource> GetCustomerTicketsByPhoneNumber(string phone, int pageIndex, int pageSize)
        {
            var tickets =await _unitOfWork.CustomerTickets.GetCustomerTicketsByPhoneNumber(phone, pageIndex , pageSize);
            return tickets;
        }

        public async Task<CustomerTickets> GetCustomerTicketsById(int Id)
        {
            var tickets = await _unitOfWork.CustomerTickets.GetCustomerTicketsById(Id);
            return tickets;
        }
    }
}
