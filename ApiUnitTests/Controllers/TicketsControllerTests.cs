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
            List<AddCustomerTicketResponseResource> customerTickets = (List<AddCustomerTicketResponseResource>)(okResult.Value?.GetType().GetProperty("customerTickets")?.GetValue(okResult.Value));
            int totalCount = (int)(okResult.Value?.GetType().GetProperty("totalCount")?.GetValue(okResult.Value));
            Assert.AreEqual(tickets.TotalCount, totalCount);
            Assert.AreEqual(expectedTickets, customerTickets);
        }

        [Test]
        public async Task GetAllCustomerTickets_InValidInput_ReturnsBadRequest()
        {
            // Arrange
            var phoneNumber = "1234567890";
            var status = 1;
            var pageIndex = 1;
            var pageSize = 10;

            _mockCustomerTicketsService.Setup(x => x.GetAllCustomerTickets(phoneNumber, status, pageIndex, pageSize)).ReturnsAsync((CustomerServiceResponseResource)null);

            // Act
            var result = await _controller.GetAllCustomerTickets(phoneNumber, status, pageIndex, pageSize);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var okResult = (BadRequestObjectResult)result;
           
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), okResult.Value);
        }

        [Test]
        public async Task GetCustomerTicketById_ValidInput_ReturnsOkWithTickets()
        {
            // Arrange
            var Id = 1;

            var tickets = new CustomerTickets()
            {
                Id = 1,
                PhoneNumber = "123",
                CustomerContact = 2,
                IssueTypeId = 3,
                UserId = "user12323",
                Status = 1,
                Source="staff",
                   
            };

            var expectedTickets = new AddCustomerTicketResponseResource
            {
                Id = 1,
                PhoneNumber = "123",
                CustomerContact = 2,
                IssueTypeId = 3,
                UserId = "user45r323",
                Status = 1,
                Source = "staff",
            };
               

            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(Id)).ReturnsAsync(tickets);
            _mockMapper.Setup(x => x.Map<CustomerTickets, AddCustomerTicketResponseResource>(tickets)).Returns(expectedTickets);

            // Act
            var result = await _controller.GetCustomerTicketById(Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            AddCustomerTicketResponseResource customerTickets = (AddCustomerTicketResponseResource)(okResult.Value?.GetType().GetProperty("customerTickets")?.GetValue(okResult.Value));
            Assert.AreEqual(expectedTickets, customerTickets);
        }

        [Test]
        public async Task GetCustomerTicketById_InValidInput_ReturnsBadRequest()
        {
            // Arrange
            var Id = 0;

            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(Id)).ReturnsAsync((CustomerTickets)null);

            // Act
            var result = await _controller.GetCustomerTicketById(Id);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var okResult = (BadRequestObjectResult)result;
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), okResult.Value);
        }

        [Test]
        public async Task GetCustomerTicketById_ThrowException_ReturnsException()
        {
            // Arrange
            var Id = 1;

            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(Id)).ThrowsAsync(new Exception());

            //Act and Assert
            Exception caughtException = null;

            try
            {
                await _controller.GetCustomerTicketById(Id);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);
        }

        [Test]
        public async Task ReplyForCustomerTicket_ValidInput_ReturnsOk()
        {
            // Arrange
            var customerTicketsRequest = new CustomerTicketReplyResource
            {
               
                UserId = "user12323",
                CustomerTicketId = 1,
                Note ="this is new note"
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

            Notes notesToInsert = new Notes()
            {
                Id=1,
                Note= customerTicketsRequest.Note,
                CustomerTicketId = customerTicketsRequest.CustomerTicketId,
                AddedBy = customerTicketsRequest.UserId

            };


            _mockUserDetailsService.Setup(x => x.GetApplicationUserById(customerTickets.UserId)).ReturnsAsync(userDetail);
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(customerTickets.Id)).ReturnsAsync(customerTickets);
            _mockNotesService.Setup(x => x.AddUpdateNotes(notesToInsert)).ReturnsAsync(notesToInsert);
            _mockSMSService.Setup(x => x.sendMessage("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockWhatsAppService.Setup(x => x.sendMessageForCustomerTicket("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockEmailSender.Setup(x => x.SendEmailAsync(userDetail.Email, "template", "username", "subject")).ReturnsAsync("Email queued successfully!");


            // Act
            var result = await _controller.ReplyForCustomerTicket(customerTicketsRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(true, status);
            Assert.AreEqual(Messages.TicketUpdated.ToDescriptionString(), message);
        }

        [Test]
        public async Task ReplyForCustomerTicket_ServicenotRetrivesUserDetails_ReturnsBadRequest()
        {
            // Arrange
            var customerTicketsRequest = new CustomerTicketReplyResource
            {

                CustomerTicketId = 1,
                Note = "this is new note"
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

            _mockUserDetailsService.Setup(x => x.GetApplicationUserById(customerTickets.UserId)).ReturnsAsync((ApplicationUser)null);
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(customerTickets.Id)).ReturnsAsync(customerTickets);

            // Act
            var result = await _controller.ReplyForCustomerTicket(customerTicketsRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(false, status);
            Assert.AreEqual(Messages.NotFound.ToDescriptionString(), message);
        }

        [Test]
        public async Task ReplyForCustomerTicket_ServiceFailedToRetriveTickets_ReturnsOk()
        {
            // Arrange
            var customerTicketsRequest = new CustomerTicketReplyResource
            {

                UserId = "user12323",
                Note = "this is new note"

            };

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

            Notes notesToInsert = new Notes()
            {
                Id = 1,
                Note = customerTicketsRequest.Note,
                CustomerTicketId = customerTicketsRequest.CustomerTicketId,
                AddedBy = customerTicketsRequest.UserId

            };


            _mockUserDetailsService.Setup(x => x.GetApplicationUserById(customerTickets.UserId)).ReturnsAsync(userDetail);
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(customerTickets.Id)).ReturnsAsync((CustomerTickets)null);
           

            // Act
            var result = await _controller.ReplyForCustomerTicket(customerTicketsRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(false, status);
            Assert.AreEqual(Messages.TicketNotFound.ToDescriptionString(), message);
        }

        [Test]
        public async Task ReplyForCustomerTicket_InValidInputNotesAreNUll_ReturnsOk()
        {
            // Arrange
            var customerTicketsRequest = new CustomerTicketReplyResource
            {

                UserId = "user12323",
                CustomerTicketId = 1,
            }; 

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

            Notes notesToInsert = new Notes()
            {
                Id = 1,
                Note = customerTicketsRequest.Note,
                CustomerTicketId = customerTicketsRequest.CustomerTicketId,
                AddedBy = customerTicketsRequest.UserId

            };

            _mockUserDetailsService.Setup(x => x.GetApplicationUserById(customerTickets.UserId)).ReturnsAsync(userDetail);
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(customerTickets.Id)).ReturnsAsync(customerTickets);
            _mockNotesService.Setup(x => x.AddUpdateNotes(notesToInsert)).ReturnsAsync(notesToInsert);
            _mockSMSService.Setup(x => x.sendMessage("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockWhatsAppService.Setup(x => x.sendMessageForCustomerTicket("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockEmailSender.Setup(x => x.SendEmailAsync(userDetail.Email, "template", "username", "subject")).ReturnsAsync("Email queued successfully!");


            // Act
            var result = await _controller.ReplyForCustomerTicket(customerTicketsRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var okResult = (BadRequestObjectResult)result;
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), okResult.Value);
        }

        [Test]
        public async Task ReplyForCustomerTicket_ThrowException_ReturnsBadRequest()
        {
            // Arrange
            var customerTicketsRequest = new CustomerTicketReplyResource
            {

                UserId = "user12323",
                CustomerTicketId = 1,
                Note = "this is new note"
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

            Notes notesToInsert = new Notes()
            {
                Id = 1,
                Note = customerTicketsRequest.Note,
                CustomerTicketId = customerTicketsRequest.CustomerTicketId,
                AddedBy = customerTicketsRequest.UserId

            };


            _mockUserDetailsService.Setup(x => x.GetApplicationUserById(customerTickets.UserId)).ThrowsAsync(new Exception());
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(customerTickets.Id)).ReturnsAsync(customerTickets);
            _mockNotesService.Setup(x => x.AddUpdateNotes(notesToInsert)).ReturnsAsync(notesToInsert);
            _mockSMSService.Setup(x => x.sendMessage("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockWhatsAppService.Setup(x => x.sendMessageForCustomerTicket("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockEmailSender.Setup(x => x.SendEmailAsync(userDetail.Email, "template", "username", "subject")).ReturnsAsync("Email queued successfully!");


            //Act and Assert
            Exception caughtException = null;

            try
            {
                await _controller.ReplyForCustomerTicket(customerTicketsRequest);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);
        }

        [Test]
        public async Task CloseCustomerTicket_ValidInput_ReturnsOk()
        {
            // Arrange
            var ticketId = 1;

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
                },
                IssueType = new IssueType { }
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
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(ticketId)).ReturnsAsync(customerTickets);
            _mockSMSService.Setup(x => x.sendMessage("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockWhatsAppService.Setup(x => x.sendMessageForCustomerTicket("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockEmailSender.Setup(x => x.SendEmailAsync(userDetail.Email, "template", "username", "subject")).ReturnsAsync("Email queued successfully!");

            // Act
            var result = await _controller.CloseCustomerTicket(ticketId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(true, status);
            Assert.AreEqual(Messages.TicketClosed.ToDescriptionString(), message);

        }

        [Test]
        public async Task CloseCustomerTicket_ServiceFailedToRetriveTicket_ReturnsOk()
        {
            // Arrange
            var ticketId = 0;

            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(ticketId)).ReturnsAsync((CustomerTickets)null);
          
            // Act
            var result = await _controller.CloseCustomerTicket(ticketId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(false, status);
            Assert.AreEqual(Messages.TicketNotFound.ToDescriptionString(), message);

        }

        [Test]
        public async Task CloseCustomerTicket_ServicenotRetrivesUserDetails()
        {
            // Arrange
            var ticketId = 1;

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
                },
                IssueType = new IssueType { }
            }; 
            
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

            _mockUserDetailsService.Setup(x => x.GetApplicationUserById(customerTickets.UserId)).ReturnsAsync((ApplicationUser)null);
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(ticketId)).ReturnsAsync(customerTickets);
           
            // Act
            var result = await _controller.CloseCustomerTicket(ticketId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(false, status);
            Assert.AreEqual(Messages.NotFound.ToDescriptionString(), message);

        }

        [Test]
        public async Task CloseCustomerTicket_ServiceFailedToUpdateTickets()
        {
            // Arrange
            var ticketId = 1;

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
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(customerTickets.Id)).ReturnsAsync(customerTickets);
            _mockSMSService.Setup(x => x.sendMessage("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockWhatsAppService.Setup(x => x.sendMessageForCustomerTicket("", userDetail.UserDetail.PhoneNumber)).Returns("some code");
            _mockEmailSender.Setup(x => x.SendEmailAsync(userDetail.Email, "template", "username", "subject")).ReturnsAsync("Email queued successfully!");


            // Act
            var result = await _controller.CloseCustomerTicket(ticketId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var okResult = (BadRequestObjectResult)result;
           
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), okResult.Value);
        }

        [Test]
        public async Task PatchReportSeenStatus_ValidInput_ReturnsOk()
        {
            // Arrange
            var ticketId = 1;
            bool isSeen = true;

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
                },
                IssueType = new IssueType { }
            }; 

            

            _mockCustomerTicketsService.Setup(x => x.AddUpdateCustomerTickets(It.IsAny<CustomerTickets>())).ReturnsAsync(customerTickets);
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(ticketId)).ReturnsAsync(customerTickets);
           
            // Act
            var result = await _controller.PatchReportSeenStatus(ticketId, isSeen);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(true, status);
            Assert.AreEqual(Messages.StatusUpdated.ToDescriptionString(), message);

        }

        [Test]
        public async Task PatchReportSeenStatus_InValidInput_ServiceFailedToRetriveTicket()
        {
            // Arrange
            var ticketId = 0;
            bool isSeen = true;

            _mockCustomerTicketsService.Setup(x => x.AddUpdateCustomerTickets(It.IsAny<CustomerTickets>())).ReturnsAsync((CustomerTickets)null);
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(ticketId)).ReturnsAsync((CustomerTickets)null);

            // Act
            var result = await _controller.PatchReportSeenStatus(ticketId, isSeen);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(false, status);
            Assert.AreEqual(Messages.TicketNotFound.ToDescriptionString(), message);

        }

        [Test]
        public async Task PatchReportSeenStatus_ServiceFailedToUpdateTickets_ReturnsOk()
        {
            // Arrange
            var ticketId = 0;
            bool isSeen = true;
            
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
                },
                IssueType = new IssueType { }
            };

            _mockCustomerTicketsService.Setup(x => x.AddUpdateCustomerTickets(It.IsAny<CustomerTickets>())).ReturnsAsync((CustomerTickets)null);
            _mockCustomerTicketsService.Setup(x => x.GetCustomerTicketsById(ticketId)).ReturnsAsync(customerTickets);

            // Act
            var result = await _controller.PatchReportSeenStatus(ticketId, isSeen);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var okResult = (BadRequestObjectResult)result;
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));
            Assert.AreEqual(false, status);
            Assert.AreEqual(Messages.ErrorOccured.ToDescriptionString(), message);

        }
    }
}
