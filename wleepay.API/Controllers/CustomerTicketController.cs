using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTicketController : Controller
    {

        private readonly ICustomerTicketsService _customerTicketsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ISMSService _sMSService;


        public CustomerTicketController(
            ICustomerTicketsService customerTickets,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ISMSService sMSService)
        {

            _customerTicketsService = customerTickets;
            _userManager = userManager;
            _mapper = mapper;
            _sMSService = sMSService;
        }

        [HttpPost("AddUpdateCustomerTickets")]
        public async Task<IActionResult> AddUpdateCustomerTickets(AddCustomerTicketResource customerTickets)
        {

            customerTickets.Status = (int)CustomerTicketStatus.Open;
            customerTickets.CreatedDate = DateTime.UtcNow;
            var customerTicketsToInsert = _mapper.Map<AddCustomerTicketResource, CustomerTickets>(customerTickets);

            var tickets = await _customerTicketsService.AddUpdateCustomerTickets(customerTicketsToInsert);
            if(tickets != null)
            {
                var message = "Customer ticked is created successfully. you will be noticed further";
                _sMSService.sendSmsForCustomerTicket(message, customerTickets.PhoneNumber);
            }
            return Ok(tickets);
        }

        [HttpGet("GetAllCustomerTickets")]
        public async Task<IActionResult> GetAllCustomerTickets()
        {
            var tickets =await _customerTicketsService.GetAllCustomerTickets();
            return Ok(tickets);
        }

        [HttpPost("GetCustomerTicketsByPhone")]
        public async Task<IActionResult> GetCustomerTicketsByPhone(CustomerTicketRequest request)
        {
            var tickets = await _customerTicketsService.GetCustomerTicketsByPhoneNumber(request.PhoneNumber);
            return Ok(tickets);
        }
    }
}
