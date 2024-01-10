using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
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
    }
}
