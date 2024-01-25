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
using wleepay.Communication.Templates;
using wleepay.Core.Common;
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
        private readonly IWhatsAppService _whatsAppService;



        public CustomerTicketController(
            ICustomerTicketsService customerTickets,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ISMSService sMSService,
            IUserDetailService userDetailsService,
            IEmailSender emailSender,
            INotesService notesService,
            IWhatsAppService whatsAppService)
        {

            _customerTicketsService = customerTickets;
            _userManager = userManager;
            _mapper = mapper;
            _sMSService = sMSService;
            _userDetailsService = userDetailsService;
            _emailSender = emailSender;
            _notesService = notesService;
            _whatsAppService = whatsAppService;
        }

        [HttpPost("AddUpdateCustomerTickets")]
        public async Task<IActionResult> AddUpdateCustomerTickets(AddCustomerTicketRequestResponse customerTickets)
        {
            try
            {
                var userDetail = await _userDetailsService.GetApplicationUserById(customerTickets.UserId);
                if (userDetail == null)
                {
                    return Ok(new
                    {
                        status = false,
                        message = Messages.NotFound.ToDescriptionString()
                    });
                }
                if (userDetail.UserDetail.PhoneNumber != customerTickets.PhoneNumber)
                {
                    return Ok(new
                    {
                        status = false,
                        message = Messages.PhoneNumberNotExisted.ToDescriptionString(),
                    });
                }

                customerTickets.Status = (int)CustomerTicketStatus.Open;
                customerTickets.CreatedDate = DateTime.UtcNow;
                var customerTicketsToInsert = _mapper.Map<AddCustomerTicketRequestResponse, CustomerTickets>(customerTickets);

                customerTicketsToInsert.IsSeen = false;
                customerTicketsToInsert.Notes.FirstOrDefault().AddedBy= userDetail.UserDetail.FirstName+ " " + userDetail.UserDetail.LastName;
                var tickets = await _customerTicketsService.AddUpdateCustomerTickets(customerTicketsToInsert);

                var ticketDetails = await _customerTicketsService.GetCustomerTicketsById(customerTicketsToInsert.Id);
                if (tickets != null)
                {
                    var message = MessagesTemplates.CustomerTicketCreatedMessage(ticketDetails.IssueType.Type, tickets.Id);
                    var subject = Messages.TicketCreated.ToDescriptionString();
                    if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Email)
                    {
                        await _emailSender.ReplyForCustomerTicket(message, userDetail.Email, subject);
                    }
                    if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Phone)
                    {
                        _sMSService.sendSmsForCustomerTicket(message, customerTickets.PhoneNumber);
                    }
                    if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Whatsapp)
                    {
                        _whatsAppService.sendMessageForCustomerTicket(message, userDetail.PhoneNumber);
                    }
                   
                }
                return Ok(new
                {
                    status = true,
                    message = Messages.CustomerTicketCreated.ToDescriptionString()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
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
                customerTickets= customerTicketsToInsert,
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
                    message = Messages.NotFound.ToDescriptionString()
                });
            }
            var ticket = await _customerTicketsService.GetCustomerTicketsById(request.CustomerTicketId);
            if (ticket == null)
            {
                return Ok(new
                {
                    status = false,
                    message = Messages.TicketNotFound.ToDescriptionString()
                });
            }
            var message = MessagesTemplates.CustomerTicketReplyMessage(userDetail.UserDetail.FirstName, request.Note);
            var subject = Messages.SubjectForTicketUpdated.ToDescriptionString();

            if (request.Note != null)
            {
                if (ticket.CustomerContact == (int)CustomerServiceContactMethod.Email)
                {
                    await _emailSender.ReplyForCustomerTicket(message, userDetail.Email, subject);
                }
                if (ticket.CustomerContact == (int)CustomerServiceContactMethod.Phone)
                {
                    _sMSService.sendSmsForCustomerTicket(message, userDetail.PhoneNumber);
                }
                if (ticket.CustomerContact == (int)CustomerServiceContactMethod.Whatsapp)
                {
                    _whatsAppService.sendMessageForCustomerTicket(message, userDetail.PhoneNumber);
                }
                return Ok(new
                {
                    status = true,
                    message = Messages.TicketClosed.ToDescriptionString(),
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
                    message = Messages.NotFound.ToDescriptionString()
                });
            }

            var ticketToUpdated = await _customerTicketsService.GetCustomerTicketsById(request.CustomerTicketId);
            if (ticketToUpdated == null)
            {
                return Ok(new
                {
                    status = false,
                    message = Messages.TicketNotFound.ToDescriptionString()
                });
            }

            ticketToUpdated.Status = (int)CustomerTicketStatus.Closed;
            ticketToUpdated.UpdatedDate = DateTime.UtcNow;
            var tickets = await _customerTicketsService.AddUpdateCustomerTickets(ticketToUpdated);

            if (tickets != null)
            {
                var notesToInsert = _mapper.Map<NotesResource, Notes>(request.Notes.FirstOrDefault());
                notesToInsert.CustomerTicketId = request.CustomerTicketId;
                var notes = await _notesService.AddUpdateNotes(notesToInsert);

                var message = MessagesTemplates.CustomerTicketReplyMessage(userDetail.UserDetail.FirstName, request.Notes.FirstOrDefault().Note);
                var subject = Messages.SubjectForTicketClosed.ToDescriptionString();

                if (notes != null)
                {

                    if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Email)
                    {
                        await _emailSender.ReplyForCustomerTicket(message, userDetail.Email, subject);
                    }
                    if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Phone)
                    {
                        _sMSService.sendSmsForCustomerTicket(message, userDetail.PhoneNumber);
                    }
                    if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Whatsapp)
                    {
                        _whatsAppService.sendMessageForCustomerTicket(message, userDetail.PhoneNumber);
                    }

                    return Ok(new
                    {
                        status = true,
                        message = Messages.TicketClosed.ToDescriptionString()
                    });
                }
                
                return Ok(new
                {
                    status = true,
                    message = Messages.TicketClosed.ToDescriptionString(),
                });

            }

            return BadRequest();
        }

        [HttpPatch("ReportSeenStatus")]
        public async Task<IActionResult> PatchReportSeenStatus(CustomerTicketSeenStatusRequest request)
        {
            try
            {
                var ticketToUpdated = await _customerTicketsService.GetCustomerTicketsById(request.CustomerTicketId);
                if (ticketToUpdated == null)
                {
                    return Ok(new
                    {
                        status = false,
                        message = Messages.TicketNotFound.ToDescriptionString()
                    });
                }

                ticketToUpdated.IsSeen = request.IsSeen;
                ticketToUpdated.UpdatedDate = DateTime.UtcNow;
                var tickets = await _customerTicketsService.AddUpdateCustomerTickets(ticketToUpdated);
                if (tickets != null)
                {
                    return Ok(new
                    {
                        status = true,
                        message = "status updated!"
                    });

                }

                return BadRequest(new
                {
                    status = false,
                    message = "Error occurred while updating status"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    message = ex.Message
                });
            }

        }


    }
}
