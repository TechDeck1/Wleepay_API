using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Helpers;
using wleepay.API.Hub;
using wleepay.API.Middleware;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Communication.Templates;
using wleepay.Core.Common;
using wleepay.Core.DTO.Request;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class TicketsController : Controller
    {

        private readonly ICustomerTicketsService _customerTicketsService;
        private readonly IUserDetailService _userDetailsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ISMSService _sMSService;
        private readonly IEmailSender _emailSender;
        private readonly INotesService _notesService;
        private readonly IWhatsAppService _whatsAppService;
        private readonly IFirebaseNotification _firebaseNotification;
        private IHubContext<TicketsNotificationHubClient, ITicketsNotificationHubClient> _notificationHub;


        public TicketsController(
            ICustomerTicketsService customerTickets,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ISMSService sMSService,
            IUserDetailService userDetailsService,
            IEmailSender emailSender,
            INotesService notesService,
            IWhatsAppService whatsAppService,
            IHubContext<TicketsNotificationHubClient, ITicketsNotificationHubClient> notificationHub,
            IFirebaseNotification firebaseNotification)
        {

            _customerTicketsService = customerTickets;
            _userManager = userManager;
            _mapper = mapper;
            _sMSService = sMSService;
            _userDetailsService = userDetailsService;
            _emailSender = emailSender;
            _notesService = notesService;
            _whatsAppService = whatsAppService;
            _notificationHub = notificationHub;
            _firebaseNotification = firebaseNotification;
        }

        [HttpPost]
        [SystemLock]
        [AllowAnonymous]
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
                customerTickets.UpdatedDate = DateTime.UtcNow;
                var customerTicketsToInsert = _mapper.Map<AddCustomerTicketRequestResponse, CustomerTickets>(customerTickets);

                customerTicketsToInsert.IsSeen = false;
                customerTicketsToInsert.UserId = customerTickets.UserId;
                customerTicketsToInsert.Source = userDetail.UserRoles.FirstOrDefault().Role.Name;
                customerTicketsToInsert.NotificationStatus = customerTickets.NotificationStatus;

                customerTicketsToInsert.Notes.FirstOrDefault().AddedBy = userDetail.UserDetail.UserId;
                var tickets = await _customerTicketsService.AddUpdateCustomerTickets(customerTicketsToInsert);

                var ticketDetails = await _customerTicketsService.GetCustomerTicketsById(customerTicketsToInsert.Id);
                if (tickets != null)
                {
                    var message = MessagesTemplates.CustomerTicketCreatedMessage(ticketDetails.IssueType.Type, tickets.Id);
                    var subject = Messages.TicketCreated.ToDescriptionString();
                    if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Email)
                    {
                        await _emailSender.SendEmailAsync(userDetail.Email, message, userDetail.UserDetail.FirstName, subject);
                    }
                    if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Phone)
                    {
                        _sMSService.sendMessage(message, customerTickets.PhoneNumber);
                    }
                    if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Whatsapp)
                    {
                        _whatsAppService.sendMessageForCustomerTicket(message, userDetail.PhoneNumber);
                    }

                }
                return Ok(new
                {
                    status = true,
                    message = tickets.Id <= 0 ? Messages.CustomerTicketCreated.ToDescriptionString() : Messages.TicketUpdated.ToDescriptionString()
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

        [HttpGet]
        public async Task<IActionResult> GetAllCustomerTickets([FromQuery]string phoneNumber, [FromQuery] int status, [FromQuery] int pageIndex, [FromQuery] int? pageSize = 10)
        {
            try
            {
                var tickets = await _customerTicketsService.GetAllCustomerTickets(phoneNumber, status, pageIndex, pageSize);
                var customerTicketsToInsert = _mapper.Map<IEnumerable<CustomerTickets>, IEnumerable<AddCustomerTicketResponseResource>>(tickets.CustomerTickets);
                var hubtest= _notificationHub.Clients.All.GetLatestNotificationForTickets(tickets.CustomerTickets);

                return Ok(new
                {
                    totalCount = tickets.TotalCount,
                    customerTickets = customerTicketsToInsert,
                });
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
           

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomerTicketById(int Id)
        {
            try
            {
                var tickets = await _customerTicketsService.GetCustomerTicketsById(Id);
                var customerTicketsToInsert = _mapper.Map<CustomerTickets, AddCustomerTicketResponseResource>(tickets);

                return Ok(new
                {
                    customerTickets = customerTicketsToInsert,
                });
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
           

        }

        [SystemLock]
        [HttpPost("reply")]
        public async Task<IActionResult> ReplyForCustomerTicket(CustomerTicketReplyResource request)
        {
            try
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

            var memberDetails = await _userDetailsService.GetApplicationUserById(request.UserId);
            if (memberDetails == null)
            {
                return Ok(new
                {
                    status = false,
                    message = Messages.NotFound.ToDescriptionString()
                });
            }

            Notes notesToInsert = new Notes();
            notesToInsert.CustomerTicketId = request.CustomerTicketId;
            notesToInsert.Note = request.Note;
            notesToInsert.AddedBy = request.UserId;
            var notes = await _notesService.AddUpdateNotes(notesToInsert);
           
            ticket.Status = (int)CustomerTicketStatus.Open;
            ticket.UpdatedDate = DateTime.UtcNow;
            ticket.NotificationStatus = (int)CustomerTicketNotificationStatus.New;
            var tickets = await _customerTicketsService.AddUpdateCustomerTickets(ticket);

            var smsMessageTemplate = MessagesTemplates.CustomerTicketReplyMessageSmsTemplate(memberDetails.UserDetail.FirstName, request.Note);
            var message = MessagesTemplates.CustomerTicketReplyMessage(memberDetails.UserDetail.FirstName, request.Note);
            var subject = Messages.SubjectForTicketUpdated.ToDescriptionString();

            FirebaseMessageRequest firebaseMessageRequest = new FirebaseMessageRequest();
            firebaseMessageRequest.Title = "Note added";
            firebaseMessageRequest.Body = "New note is added for ticket number "+ticket.Id;
            firebaseMessageRequest.DeviceToken = userDetail.UserDetail.FcmToken;
            firebaseMessageRequest.DeviceId = userDetail.UserDetail.DeviceId;
            firebaseMessageRequest.UserId = userDetail.UserDetail.UserId;
            firebaseMessageRequest.FirstName = userDetail.UserDetail.FirstName;
            firebaseMessageRequest.LastName = userDetail.UserDetail.LastName;

            var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);


                if (request.Note != null)
                {

                if (ticket.CustomerContact == (int)CustomerServiceContactMethod.Email)
                {
                    //await _emailSender.ReplyForCustomerTicket(message, memberDetails.Email, subject);
                    await _emailSender.SendEmailAsync(userDetail.Email, message, userDetail.UserDetail.FirstName, subject);
                }
                if (ticket.CustomerContact == (int)CustomerServiceContactMethod.Phone)
                {
                    _sMSService.sendMessage(smsMessageTemplate, memberDetails.PhoneNumber);
                }
                if (ticket.CustomerContact == (int)CustomerServiceContactMethod.Whatsapp)
                {
                    _whatsAppService.sendMessageForCustomerTicket(smsMessageTemplate, memberDetails.PhoneNumber);
                }
                return Ok(new
                {
                    status = true,
                    message = Messages.TicketUpdated.ToDescriptionString(),
                });
            }

            return BadRequest();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [SystemLock]
        [HttpPost("{ticketId}/close")]
        public async Task<IActionResult> CloseCustomerTicket(int ticketId)
        {
            try
            {
            var ticketToUpdated = await _customerTicketsService.GetCustomerTicketsById(ticketId);
            if (ticketToUpdated == null)
            {
                return Ok(new
                {
                    status = false,
                    message = Messages.TicketNotFound.ToDescriptionString()
                });
            }
            var userDetail = await _userDetailsService.GetApplicationUserById(ticketToUpdated.UserId);
            if (userDetail == null)
            {
                return Ok(new
                {
                    status = false,
                    message = Messages.NotFound.ToDescriptionString()
                });
            }

            ticketToUpdated.Status = (int)CustomerTicketStatus.Closed;
            ticketToUpdated.UpdatedDate = DateTime.UtcNow;
            ticketToUpdated.NotificationStatus = (int)CustomerTicketNotificationStatus.New;

            var tickets = await _customerTicketsService.AddUpdateCustomerTickets(ticketToUpdated);

            var allTickets = await _customerTicketsService.GetCustomerTicketsByUserId(tickets.UserId);

            _notificationHub.Clients.All.GetLatestNotificationForTickets(allTickets);

            if (tickets != null)
            {
                FirebaseMessageRequest firebaseMessageRequest = new FirebaseMessageRequest();
                firebaseMessageRequest.Title = "Ticket is closed";
                firebaseMessageRequest.Body= " Ticket is closed for issue type " + ticketToUpdated.IssueType.Description;
                firebaseMessageRequest.DeviceToken = userDetail.UserDetail.FcmToken;
                firebaseMessageRequest.DeviceId = userDetail.UserDetail.DeviceId;
                firebaseMessageRequest.UserId = userDetail.UserDetail.UserId;
                firebaseMessageRequest.FirstName = userDetail.UserDetail.FirstName;
                firebaseMessageRequest.LastName = userDetail.UserDetail.LastName;
               
                var sendNotification = _firebaseNotification.SendFirebaseMessageRequest(firebaseMessageRequest);
                
                var message = MessagesTemplates.CustomerTicketClosedMessage(ticketToUpdated.IssueType.Description, ticketId);
                var subject = Messages.SubjectForTicketClosed.ToDescriptionString();

                if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Email)
                {
                    //await _emailSender.ReplyForCustomerTicket(message, userDetail.Email, subject);
                    await _emailSender.SendEmailAsync(userDetail.Email, message, userDetail.UserDetail.FirstName, subject);

                }
                if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Phone)
                {
                    _sMSService.sendMessage(message, userDetail.PhoneNumber);
                }
                if (tickets.CustomerContact == (int)CustomerServiceContactMethod.Whatsapp)
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
            catch (Exception ex)
            {
                throw new CustomException("Sorry, we're having trouble in closing ticket. Please contact support.");
            }

        }

        [SystemLock]
        [HttpPatch("{ticketId}/status")]
        public async Task<IActionResult> PatchReportSeenStatus(int ticketId, [FromQuery] bool isSeen)
        {
            try
            {
                var ticketToUpdated = await _customerTicketsService.GetCustomerTicketsById(ticketId);
                if (ticketToUpdated == null)
                {
                    return Ok(new
                    {
                        status = false,
                        message = Messages.TicketNotFound.ToDescriptionString()
                    });
                }

                ticketToUpdated.IsSeen = isSeen;
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

        [HttpGet("{Id}/userId")]
        public async Task<IActionResult> GetCustomerTicketsByUserId(string Id)
        {
            try
            {
                var tickets = await _customerTicketsService.GetCustomerTicketsByUserId(Id);
                _notificationHub.Clients.All.GetLatestNotificationForTickets(tickets);

                var customerTickets = _mapper.Map<IEnumerable<CustomerTickets>, IEnumerable<AddCustomerTicketResponseResource>>(tickets);

                return Ok(new
                {
                    customerTickets = customerTickets,
                });
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }


        }

    }
}
