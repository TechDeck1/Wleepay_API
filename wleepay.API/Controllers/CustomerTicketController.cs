using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
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
        private readonly INotesService _notesService;


        public CustomerTicketController(
            ICustomerTicketsService customerTickets,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ISMSService sMSService,
            IUserDetailService userDetailsService,
            IEmailSender emailSender,
            INotesService notesService)
        {

            _customerTicketsService = customerTickets;
            _userManager = userManager;
            _mapper = mapper;
            _sMSService = sMSService;
            _userDetailsService = userDetailsService;
            _emailSender = emailSender;
            _notesService = notesService;
        }

        [HttpPost("AddUpdateCustomerTickets")]
        public async Task<IActionResult> AddUpdateCustomerTickets(AddCustomerTicketRequestResponse customerTickets)
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
                var customerTicketsToInsert = _mapper.Map<AddCustomerTicketRequestResponse, CustomerTickets>(customerTickets);

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

        [HttpPost("GetAllCustomerTickets")]
        public async Task<IActionResult> GetAllCustomerTickets(CustomerTicketRequest request)
        {
            var tickets = await _customerTicketsService.GetAllCustomerTickets(request.PhoneNumber, request.PageIndex, request.PageSize);
            var customerTicketsToInsert = _mapper.Map<IEnumerable<CustomerTickets>, IEnumerable<AddCustomerTicketResponseResource>>(tickets.CustomerTickets);

            return Ok(new 
            {
                totalCount=tickets.TotalCount,
                customerTickets= customerTicketsToInsert
            });

        }

        [HttpPost("GetCustomerTicketsByPhone")]
        public async Task<IActionResult> GetCustomerTicketsByPhone(CustomerTicketRequest request)
        {
            var tickets = await _customerTicketsService.GetCustomerTicketsByPhoneNumber(request.PhoneNumber, request.PageIndex, request.PageSize);
            var customerTicketsToInsert = _mapper.Map<IEnumerable<CustomerTickets>, IEnumerable<AddCustomerTicketResponseResource>>(tickets.CustomerTickets);

            return Ok(new
            {
                totalCount = tickets.TotalCount,
                customerTickets = customerTicketsToInsert
            });
        }

        [HttpPost("ReplyForCustomerTicket")]
        public async Task<IActionResult> ReplyForCustomerTicket(CustomerTicketReplyResource request)
        {
            var userDetail = await _userDetailsService.GetApplicationUserById(request.UserId);
            if (userDetail == null)
            {
                return Ok(new
                {
                    status = false,
                    message = "User does not exist",
                });
            }
            var ticket = await _customerTicketsService.GetCustomerTicketsById(request.CustomerTicketId);
            if (ticket == null)
            {
                return Ok(new
                {
                    status = false,
                    message = "ticket does not exist.",
                });
            }

            if (request.Note != null)
            {
                if (ticket.CustomerContact == (int)CustomerServiceContactMethod.Email)
                {
                    await _emailSender.ReplyForCustomerTicket(request.Note, userDetail.Email);
                }
                if (ticket.CustomerContact == (int)CustomerServiceContactMethod.Phone)
                {
                    _sMSService.replyCustomerForCustomerTicket(request.Note, userDetail.PhoneNumber);
                }
                return Ok(new
                {
                    status = true,
                    message = "Reply sent!",
                });
            }

            return BadRequest();
        }

        [HttpPost("CloseCustomerTicket")]
        public async Task<IActionResult> CloseCustomerTicket(CustomerTicketCloseResource request)
        {
            var userDetail = await _userDetailsService.GetApplicationUserById(request.UserId);
            if (userDetail == null)
            {
                return Ok(new
                {
                    status = false,
                    message = "User does not exist",
                });
            }

            var ticketToUpdated = await _customerTicketsService.GetCustomerTicketsById(request.CustomerTicketId);
            if (ticketToUpdated == null)
            {
                return Ok(new
                {
                    status = false,
                    message = "ticket does not exist.",
                });
            }

            ticketToUpdated.Status = (int)CustomerTicketStatus.Closed;
            ticketToUpdated.UpdatedDate = DateTime.UtcNow;
            var tickets = await _customerTicketsService.AddUpdateCustomerTickets(ticketToUpdated);

            if (tickets != null)
            {
                var notesToInsert = _mapper.Map<NotesResource, Notes>(request.Notes.FirstOrDefault());
                var notes = await _notesService.AddUpdateNotes(notesToInsert);
                if (notes != null)
                {
                    if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Email)
                    {
                        await _emailSender.ReplyForCustomerTicket(request.Notes.FirstOrDefault().Note, userDetail.Email);
                    }
                    if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Phone)
                    {
                        _sMSService.replyCustomerForCustomerTicket(request.Notes.FirstOrDefault().Note, userDetail.PhoneNumber);
                    }
                    return Ok(new
                    {
                        status = true,
                        message = "Ticket is updated",
                    });
                }

                var message = "Customer ticked is closed successfully";
                _sMSService.sendSmsForCustomerTicket(message, ticketToUpdated.PhoneNumber);
                return Ok(new
                {
                    status = true,
                    message = "Ticket is closed",
                });

            }

            return BadRequest();
        }
    }
}
