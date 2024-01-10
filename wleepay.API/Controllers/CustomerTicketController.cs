using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using wleepay.API.Resources;
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


        public CustomerTicketController(
            ICustomerTicketsService customerTickets,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {

            _customerTicketsService = customerTickets;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("AddUpdateCustomerTickets")]
        public async Task<IActionResult> AddUpdateCustomerTickets(AddCustomerTicketResource customerTickets)
        {

            customerTickets.Status = (int)CustomerTicketStatus.Open;
            customerTickets.CreatedDate = DateTime.UtcNow;
            var customerTicketsToInsert = _mapper.Map<AddCustomerTicketResource, CustomerTickets>(customerTickets);

            var tickets = await _customerTicketsService.AddUpdateCustomerTickets(customerTicketsToInsert);
            return Ok(tickets);
        }
    }
}
