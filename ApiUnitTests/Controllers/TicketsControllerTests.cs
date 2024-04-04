using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.API.Controllers;
using wleepay.API.Hub;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Communication.Services;
using wleepay.Core.Common;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;

namespace ApiUnitTests.Controllers
{
    public class TicketsControllerTests
    {
        private TicketsController _controller;
        private Mock<ISystemStatusService> _mockSystemStatusService;
        private Mock<IMapper> _mockMapper;
        private Mock<IValidator<AddStaffDetailsResource>> _mockValidator;
        private Mock<ICustomerTicketsService> _mockCustomerTicketsService;
        private Mock<IUserDetailService> _mockUserDetailsService;
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private Mock<RoleManager<ApplicationRole>> _mockRoleManager;
        private Mock<ISMSService> _mockSMSService;
        private Mock<IEmailSender> _mockEmailSender;
        private Mock<INotesService> _mockNotesService;
        private Mock<IWhatsAppService> _mockWhatsAppService;


        [SetUp]
        public void Setup()
        {
            _mockSystemStatusService = new Mock<ISystemStatusService>();
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<AddStaffDetailsResource>>();
            _mockCustomerTicketsService = new Mock<ICustomerTicketsService>();
            _mockUserDetailsService = new Mock<IUserDetailService>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _mockRoleManager = new Mock<RoleManager<ApplicationRole>>(Mock.Of<IRoleStore<ApplicationRole>>(), null, null, null, null); 
            _mockSMSService = new Mock<ISMSService>();
            _mockEmailSender = new Mock<IEmailSender> ();
            _mockNotesService = new Mock<INotesService> ();
            _mockWhatsAppService = new Mock<IWhatsAppService> ();

        _controller = new TicketsController(
            _mockCustomerTicketsService.Object,
            _mockUserManager.Object,
            _mockMapper.Object,
            _mockSMSService.Object,
            _mockUserDetailsService.Object,
            _mockEmailSender.Object,
            _mockNotesService.Object,
            _mockWhatsAppService.Object
         );

        }
        [Test]
        public async Task AddUpdateCustomerTickets_ValidInput_ReturnsOk()
        {
            // Arrange
            var customerTicketsRequest = new AddCustomerTicketRequestResponse
            {
                PhoneNumber = "123",
                CustomerContact= 2,
                IssueTypeId= 3,
                UserId = "user12323",
                Status = 1,
                Notes = new List<NotesResource>
                {
                    new NotesResource { Note = "First note" },
                }

            }; // Sample valid input

            var customerTickets = new CustomerTickets
            {
                Id = 1,
                PhoneNumber = "123",
                CustomerContact = 2,
                IssueTypeId = 3,
                UserId = "user12323",
                Status = 1,
                Source="staff",
                Notes = new List<Notes>
                {
                    new Notes { Note = "First note" },
                }

            }; // Sample valid input
            var userDetail = new ApplicationUser()
            {
                Email="test@example.com",
                UserDetail = new UserDetail { UserId="523455", PhoneNumber="123" },
                UserRoles = new List<ApplicationUserRole>
                {
                    new ApplicationUserRole { },
                    // Add more roles if needed
                }
            }; 
           
            _mockUserDetailsService.Setup(x => x.GetApplicationUserById(customerTickets.UserId)).ReturnsAsync(userDetail);
            _mockCustomerTicketsService.Setup(x => x.AddUpdateCustomerTickets(It.IsAny<CustomerTickets>())).ReturnsAsync(customerTickets);
            _mockMapper.Setup(x => x.Map<AddCustomerTicketRequestResponse, CustomerTickets>(customerTicketsRequest)).Returns(customerTickets);
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(customerTickets.Id)).ReturnsAsync(customerTickets);
            _mockSMSService.Setup(x => x.sendMessage("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockWhatsAppService.Setup(x => x.sendMessageForCustomerTicket("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockEmailSender.Setup(x => x.SendEmailAsync(userDetail.Email,"template","username","subject")).ReturnsAsync("Email queued successfully!");


            // Act
            var result = await _controller.AddUpdateCustomerTickets(customerTicketsRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(true, status);
            Assert.AreEqual(Messages.CustomerTicketCreated.ToDescriptionString(), message);
        }

        [Test]
        public async Task AddUpdateCustomerTickets_ServicenotRetrivesUserDetails_ReturnsOk()
        {
            // Arrange
            var customerTicketsRequest = new AddCustomerTicketRequestResponse
            {
                PhoneNumber = "123",
                CustomerContact = 2,
                IssueTypeId = 3,
                UserId = "user12323",
                Status = 1,
                Notes = new List<NotesResource>
                {
                    new NotesResource { Note = "First note" },
                }

            }; // Sample valid input

            _mockUserDetailsService.Setup(x => x.GetApplicationUserById(customerTicketsRequest.UserId)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.AddUpdateCustomerTickets(customerTicketsRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(false, status);
            Assert.AreEqual(Messages.NotFound.ToDescriptionString(), message);
        }

        [Test]
        public async Task AddUpdateCustomerTickets_PhoneNumberNotExistsOrNotSame_ReturnsOk()
        {
            // Arrange
            var customerTicketsRequest = new AddCustomerTicketRequestResponse
            {
                PhoneNumber= "345",
                CustomerContact = 2,
                IssueTypeId = 3,
                UserId = "user12323",
                Status = 1,
                Notes = new List<NotesResource>
                {
                    new NotesResource { Note = "First note" },
                }

            }; // Sample valid input

            var userDetail = new ApplicationUser()
            {
                Email = "test@example.com",
                UserDetail = new UserDetail { UserId = "523455", PhoneNumber = "123" },
                UserRoles = new List<ApplicationUserRole>
                {
                    new ApplicationUserRole { },
                    // Add more roles if needed
                }
            };

            _mockUserDetailsService.Setup(x => x.GetApplicationUserById(customerTicketsRequest.UserId)).ReturnsAsync(userDetail);

            // Act
            var result = await _controller.AddUpdateCustomerTickets(customerTicketsRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(false, status);
            Assert.AreEqual(Messages.PhoneNumberNotExisted.ToDescriptionString(), message);
        }

        [Test]
        public async Task AddUpdateCustomerTickets_ServiceFailedToAddTickets_ReturnsOk()
        {
            // Arrange
            var customerTicketsRequest = new AddCustomerTicketRequestResponse
            {
                PhoneNumber = "123",
                CustomerContact = 2,
                IssueTypeId = 3,
                UserId = "user12323",
                Status = 1,
                Notes = new List<NotesResource>
                {
                    new NotesResource { Note = "First note" },
                }

            }; // Sample valid input

            var customerTickets = new CustomerTickets
            {
                Id = 1,
                PhoneNumber = "123",
                CustomerContact = 2,
                IssueTypeId = 3,
                UserId = "user12323",
                Status = 1,
                Source = "staff",
                Notes = new List<Notes>
                {
                    new Notes { Note = "First note" },
                }

            }; // Sample valid input
            var userDetail = new ApplicationUser()
            {
                Email = "test@example.com",
                UserDetail = new UserDetail { UserId = "523455", PhoneNumber = "123" },
                UserRoles = new List<ApplicationUserRole>
                {
                    new ApplicationUserRole { },
                    // Add more roles if needed
                }
            };

            _mockUserDetailsService.Setup(x => x.GetApplicationUserById(customerTickets.UserId)).ReturnsAsync(userDetail);
            _mockCustomerTicketsService.Setup(x => x.AddUpdateCustomerTickets(It.IsAny<CustomerTickets>())).ReturnsAsync((CustomerTickets)null);
            _mockMapper.Setup(x => x.Map<AddCustomerTicketRequestResponse, CustomerTickets>(customerTicketsRequest)).Returns(customerTickets);
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(customerTickets.Id)).ReturnsAsync(customerTickets);
            _mockSMSService.Setup(x => x.sendMessage("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockWhatsAppService.Setup(x => x.sendMessageForCustomerTicket("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockEmailSender.Setup(x => x.SendEmailAsync(userDetail.Email, "template", "username", "subject")).ReturnsAsync("Email queued successfully!");


            // Act
            var result = await _controller.AddUpdateCustomerTickets(customerTicketsRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var okResult = (BadRequestObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(false, status);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), message);
        }

        [Test]
        public async Task AddUpdateCustomerTickets_ServiceFailedToRetriveTickets_ReturnsOk()
        {
            // Arrange
            var customerTicketsRequest = new AddCustomerTicketRequestResponse
            {
                PhoneNumber = "123",
                CustomerContact = 2,
                IssueTypeId = 3,
                UserId = "user12323",
                Status = 1,
                Notes = new List<NotesResource>
                {
                    new NotesResource { Note = "First note" },
                }

            }; // Sample valid input

            var customerTickets = new CustomerTickets
            {
                Id = 1,
                PhoneNumber = "123",
                CustomerContact = 2,
                IssueTypeId = 3,
                UserId = "user12323",
                Status = 1,
                Source = "staff",
                Notes = new List<Notes>
                {
                    new Notes { Note = "First note" },
                }

            }; // Sample valid input
            var userDetail = new ApplicationUser()
            {
                Email = "test@example.com",
                UserDetail = new UserDetail { UserId = "523455", PhoneNumber = "123" },
                UserRoles = new List<ApplicationUserRole>
                {
                    new ApplicationUserRole { },
                    // Add more roles if needed
                }
            };

            _mockUserDetailsService.Setup(x => x.GetApplicationUserById(customerTickets.UserId)).ReturnsAsync(userDetail);
            _mockCustomerTicketsService.Setup(x => x.AddUpdateCustomerTickets(It.IsAny<CustomerTickets>())).ReturnsAsync(customerTickets);
            _mockMapper.Setup(x => x.Map<AddCustomerTicketRequestResponse, CustomerTickets>(customerTicketsRequest)).Returns(customerTickets);
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(customerTickets.Id)).ReturnsAsync((CustomerTickets)null);
            _mockSMSService.Setup(x => x.sendMessage("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockWhatsAppService.Setup(x => x.sendMessageForCustomerTicket("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockEmailSender.Setup(x => x.SendEmailAsync(userDetail.Email, "template", "username", "subject")).ReturnsAsync("Email queued successfully!");


            // Act
            var result = await _controller.AddUpdateCustomerTickets(customerTicketsRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var okResult = (BadRequestObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(false, status);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), message);
        }

        [Test]
        public async Task GetAllCustomerTickets_ValidInput_ReturnsOkWithTickets()
        {
            // Arrange
            var phoneNumber = "1234567890";
            var status = 1;
            var pageIndex = 1;
            var pageSize = 10;
           
            var tickets = new CustomerServiceResponseResource()
            {
                TotalCount = 2,
                CustomerTickets = new List<CustomerTickets>
                {
                    new CustomerTickets 
                    { 
                        Id = 1,
                        PhoneNumber = "123",
                        CustomerContact = 2,
                        IssueTypeId = 3,
                        UserId = "user12323",
                        Status = 1,
                        Source="staff", 
                    },
                    new CustomerTickets
                    {
                        Id = 2,
                        PhoneNumber = "123",
                        CustomerContact = 2,
                        IssueTypeId = 3,
                        UserId = "user45r323",
                        Status = 1,
                        Source="staff",
                    },
                }
            };

            var expectedTickets = new List<AddCustomerTicketResponseResource>() 
            { 
                new AddCustomerTicketResponseResource {
                    Id = 1,
                    PhoneNumber = "123",
                    CustomerContact = 2,
                    IssueTypeId = 3,
                    UserId = "user45r323",
                    Status = 1,
                    Source="staff", 
                },
                new AddCustomerTicketResponseResource {
                    Id = 2,
                    PhoneNumber = "123",
                    CustomerContact = 2,
                    IssueTypeId = 3,
                    UserId = "user12323",
                    Status = 1,
                    Source="staff", 
                }

            }; // Sample expected response
          
            _mockCustomerTicketsService.Setup(x => x.GetAllCustomerTickets(phoneNumber, status, pageIndex, pageSize)).ReturnsAsync(tickets);
            _mockMapper.Setup(x => x.Map<IEnumerable<CustomerTickets>, IEnumerable<AddCustomerTicketResponseResource>>(tickets.CustomerTickets)).Returns(expectedTickets);

            // Act
            var result = await _controller.GetAllCustomerTickets(phoneNumber, status, pageIndex, pageSize);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            List<AddCustomerTicketResponseResource> customerTickets = (List<AddCustomerTicketResponseResource>)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            int totalCount = (int)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(tickets.TotalCount, totalCount);
            Assert.AreEqual(expectedTickets, customerTickets);
        }
    }
}
