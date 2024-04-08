using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.API.Controllers;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace ApiUnitTests.Controllers
{
    public class TransactionControllerTests
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
            _mockEmailSender = new Mock<IEmailSender>();
            _mockNotesService = new Mock<INotesService>();
            _mockWhatsAppService = new Mock<IWhatsAppService>();

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
    }
}
