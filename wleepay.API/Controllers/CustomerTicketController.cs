using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTicketController : Controller
    {

        private readonly ICustomerTicketsService _customerTicketsService;
        private readonly IUserDetailService _userDetailsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ISMSService _sMSService;
        private readonly IEmailSender _emailSender;


        public CustomerTicketController(
            ICustomerTicketsService customerTickets,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ISMSService sMSService,
            IUserDetailService userDetailsService,
            IEmailSender emailSender)
        {

            _customerTicketsService = customerTickets;
            _userManager = userManager;
            _mapper = mapper;
            _sMSService = sMSService;
            _userDetailsService = userDetailsService;
            _emailSender = emailSender;
        }

        [HttpPost("AddUpdateCustomerTickets")]
        public async Task<IActionResult> AddUpdateCustomerTickets(AddCustomerTicketResource customerTickets)
        {
            try
            {
                var userDetail = await _userDetailsService.GetUserDetailsById(customerTickets.UserId);
                if (userDetail == null)
                {
                    return Ok(new
                    {
                        status = false,
                        message = "User does not exist",
                    });
                }
                if (userDetail.PhoneNumber != customerTickets.PhoneNumber)
                {
                    return Ok(new
                    {
                        status = false,
                        message = "Phone number does not exist. please enter registered number",
                    });
                }

                customerTickets.Status = (int)CustomerTicketStatus.Open;
                customerTickets.CreatedDate = DateTime.UtcNow;
                var customerTicketsToInsert = _mapper.Map<AddCustomerTicketResource, CustomerTickets>(customerTickets);

                var tickets = await _customerTicketsService.AddUpdateCustomerTickets(customerTicketsToInsert);
                if (tickets != null)
                {
                    var message = "Customer ticked is created successfully. you will be noticed further";
                    _sMSService.sendSmsForCustomerTicket(message, customerTickets.PhoneNumber);
                }
                return Ok(new
                {
                    status = true,
                    message = "Customer ticket is succesfully generated",
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = false,
                    message = ex.Message.ToString(),
                });
            }
           
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

        [HttpPost("ReplyForCustomerTicket")]
        public async Task<IActionResult> ReplyForCustomerTicket(CustomerTicketReplyResource request)
        {
            var userDetail = await _userDetailsService.GetUserDetailsById(request.UserId);
            if (userDetail == null)
            {
                return Ok(new
                {
                    status = false,
                    message = "User does not exist",
                });
            }
            var ticketToUpdate = await _customerTicketsService.GetCustomerTicketsById(request.CustomerTicketId);
            if(ticketToUpdate == null)
            {
                return Ok(new
                {
                    status = false,
                    message = "Phone number does not exist.",
                });
            }
            ticketToUpdate.UpdatedDate = DateTime.UtcNow;
            ticketToUpdate.Id = request.CustomerTicketId;

            var tickets = await _customerTicketsService.AddUpdateCustomerTickets(ticketToUpdate);
            if(tickets != null)
            {
                await _emailSender.ReplyForCustomerTicket(request.Notes.FirstOrDefault().Note, userDetail.ApplicationUser.Email, userDetail.FirstName);
                return Ok(new
                {
                    status = true,
                    message = "Ticket is updated",
                });
            }

            return BadRequest();
        }
    }
}
