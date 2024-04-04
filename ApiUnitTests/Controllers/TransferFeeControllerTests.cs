using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.API.Areas.Portal.Controllers;
using wleepay.API.Controllers;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Core.Common;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;

namespace ApiUnitTests.Controllers
{
    public class TransferFeeControllerTests
    {
        private TransferFeeController _controller;
        private Mock<IUserDetailService> _mockUserDetailsService;
        private Mock<ISMSService> _mockSMSService;
        private Mock<IMapper> _mockMapper;
        private Mock<ITransferFeeService> _mockTransferFeeService;
        private Mock<ITransferTypeService> _mockTransferTypeService;

        [SetUp]
        public void Setup()
        {
            _mockUserDetailsService = new Mock<IUserDetailService>();
            _mockSMSService = new Mock<ISMSService>();
            _mockMapper = new Mock<IMapper>();
            _mockTransferFeeService = new Mock<ITransferFeeService>();
            _mockTransferTypeService = new Mock<ITransferTypeService>();

            _controller = new TransferFeeController(
                _mockUserDetailsService.Object,
                _mockTransferFeeService.Object,
                _mockMapper.Object,
                _mockSMSService.Object,
                _mockTransferTypeService.Object
            );

        }

        [Test]
        public async Task UpdateTaxRates_ValidModel_SuccessfullyUpdatesTaxRates()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 10, // Sample organization fee
                TaxAmount=0,
                AgentCommission=0,
            };

            var TransferFeeRates = new TransferFee {
                Id = 1,
                TaxAmount = 5.0, // Sample tax amount
                OrganizationFee = 10.0, // Sample organization fee
                AgentCommission = 3.0, // Sample agent commission
                Description = "Transfer Fee Description 1",
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            var transferType = new TransferType
            {
                Id = 1,
                Name = "Transfer Type 1",
                FromWalletType = 1, // Sample from wallet type ID
                ToWalletType = 2, // Sample to wallet type ID
                Description = "Description for Transfer Type 1",
                TransferFee = 1, // Sample transfer fee ID
              
            };
            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ReturnsAsync(TransferFeeRates);
            _mockTransferFeeService.Setup(x => x.AddTransferFee(It.IsAny<TransferFee>())).ReturnsAsync(TransferFeeRates);
            _mockTransferTypeService.Setup(x => x.GetTransferTypeById(It.IsAny<int>())).ReturnsAsync(transferType);
            _mockTransferTypeService.Setup(x => x.UpdateTransferTypeById(It.IsAny<TransferType>())).ReturnsAsync(transferType);
            _mockUserDetailsService.Setup(x => x.GetUserByRole(It.IsAny<int>())).ReturnsAsync(new List<ApplicationUser>());
            _mockMapper.Setup(x=>x.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(new List<ApplicationUser>()));


            // Act
            var result = await _controller.UpdateTaxRates(taxRateResourceModel);

            // Assert
            var okResult = result as OkObjectResult;
            var updatedRates = okResult.Value as TransferFee;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsInstanceOf<TransferFee>(okResult.Value);
            Assert.AreEqual(TransferFeeRates, updatedRates);

            _mockSMSService.Verify(x => x.sendMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0)); // Verify if sendMessage method is not called if no agents found

        }

        [Test]
        public async Task UpdateTaxRates_OrganizationFeeZero_ReturnsBadRequest()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 0,
                TaxAmount = 0,
                AgentCommission = 0,
            };

            // Act
            var result = await _controller.UpdateTaxRates(taxRateResourceModel);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), badRequestResult.Value);
        }

        [Test]
        public async Task UpdateTaxRates_ServiceFailsToRetrieveTaxRates_ReturnsBadRequest()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 10, // Sample organization fee
                TaxAmount = 0,
                AgentCommission = 0,
            };

            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ReturnsAsync((TransferFee)null);

            // Act
            var result = await _controller.UpdateTaxRates(taxRateResourceModel);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), badRequestResult.Value);
        }

        [Test]
        public async Task UpdateTaxRates_ServiceFailsToAddTransferFee_ReturnsBadRequest()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 10, // Sample organization fee
                TaxAmount = 0,
                AgentCommission = 0,
            };

            var TransferFeeRates = new TransferFee
            {
                Id = 1,
                TaxAmount = 5.0, // Sample tax amount
                OrganizationFee = 10.0, // Sample organization fee
                AgentCommission = 3.0, // Sample agent commission
                Description = "Transfer Fee Description 1",
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ReturnsAsync(TransferFeeRates);
            _mockTransferFeeService.Setup(x => x.AddTransferFee(It.IsAny<TransferFee>())).ReturnsAsync((TransferFee)null);
            _mockMapper.Setup(x => x.Map<TaxRateResourceModel, TransferFee>(taxRateResourceModel)).Returns((TransferFee)null);

            // Act
            var result = await _controller.UpdateTaxRates(taxRateResourceModel);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), badRequestResult.Value);
        }

        [Test]
        public async Task UpdateTaxRates_ServiceFailsToRetrieveTransferType_ReturnsBadRequest()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 10, // Sample organization fee
                TaxAmount = 0,
                AgentCommission = 0,
            };

            var TransferFeeRates = new TransferFee
            {
                Id = 1,
                TaxAmount = 5.0, // Sample tax amount
                OrganizationFee = 10.0, // Sample organization fee
                AgentCommission = 3.0, // Sample agent commission
                Description = "Transfer Fee Description 1",
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ReturnsAsync(TransferFeeRates);
            _mockTransferFeeService.Setup(x => x.AddTransferFee(It.IsAny<TransferFee>())).ReturnsAsync(TransferFeeRates);
            _mockMapper.Setup(x => x.Map<TaxRateResourceModel, TransferFee>(taxRateResourceModel)).Returns(TransferFeeRates);
            _mockTransferTypeService.Setup(x => x.GetTransferTypeById(It.IsAny<int>())).ReturnsAsync((TransferType)null);


            // Act
            var result = await _controller.UpdateTaxRates(taxRateResourceModel);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), badRequestResult.Value);
        }

        [Test]
        public async Task UpdateTaxRates_ValidModelAndIfSmsServiceCalled_SuccessfullyUpdatesTaxRates()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 10, // Sample organization fee
                TaxAmount = 0,
                AgentCommission = 0,
            };

            var TransferFeeRates = new TransferFee
            {
                Id = 1,
                TaxAmount = 5.0, // Sample tax amount
                OrganizationFee = 10.0, // Sample organization fee
                AgentCommission = 3.0, // Sample agent commission
                Description = "Transfer Fee Description 1",
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            var transferType = new TransferType
            {
                Id = 1,
                Name = "Transfer Type 1",
                FromWalletType = 1, // Sample from wallet type ID
                ToWalletType = 2, // Sample to wallet type ID
                Description = "Description for Transfer Type 1",
                TransferFee = 1, // Sample transfer fee ID

            };

            IEnumerable<ApplicationUser> applicationUsers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "user1",
                    Email = "user1@example.com",
                    PhoneNumber = "1234567890",
                    UserDetail = new UserDetail {PhoneNumber = "1234567890",FirstName="john"  },

                },
                new ApplicationUser
                {
                    Id = "2",
                    UserName = "user2",
                    Email = "user2@example.com",
                    PhoneNumber = "9876543210",
                    UserDetail = new UserDetail {PhoneNumber = "9876543210", FirstName="john" },

                },
                new ApplicationUser
                {
                    Id = "3",
                    UserName = "user3",
                    Email = "user3@example.com",
                    PhoneNumber = "5555555555",
                    UserDetail = new UserDetail {PhoneNumber = "5555555555",FirstName="john"  },
                    // Add other properties as needed
              
                }
            };

            IEnumerable<ApplicationUserResource> applicationResourceUsers = new List<ApplicationUserResource>
            {
                new ApplicationUserResource
                {
                    UserName = "user1",
                    Email = "user1@example.com",
                    UserDetail = new UserDetailResource { PhoneNumber = "1234567890",FirstName="john"  },

                },
                new ApplicationUserResource
                {
                    UserName = "user2",
                    Email = "user2@example.com",
                    UserDetail = new UserDetailResource { PhoneNumber = "9876543210", FirstName="jyui"  },

                },
                new ApplicationUserResource
                {
                    UserName = "user3",
                    Email = "user3@example.com",
                    
                    UserDetail = new UserDetailResource { PhoneNumber = "5555555555",FirstName="yuyu"  },
              
                }
            };

            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ReturnsAsync(TransferFeeRates);
            _mockTransferFeeService.Setup(x => x.AddTransferFee(It.IsAny<TransferFee>())).ReturnsAsync(TransferFeeRates);
            _mockTransferTypeService.Setup(x => x.GetTransferTypeById(It.IsAny<int>())).ReturnsAsync(transferType);
            _mockTransferTypeService.Setup(x => x.UpdateTransferTypeById(It.IsAny<TransferType>())).ReturnsAsync(transferType);
            _mockUserDetailsService.Setup(x => x.GetUserByRole(It.IsAny<int>())).ReturnsAsync(applicationUsers);
            _mockMapper.Setup(x => x.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(applicationUsers)).Returns(applicationResourceUsers);


            // Act
            var result = await _controller.UpdateTaxRates(taxRateResourceModel);

            // Assert
            var okResult = result as OkObjectResult;
            var updatedRates = okResult.Value as TransferFee;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsInstanceOf<TransferFee>(okResult.Value);
            Assert.AreEqual(TransferFeeRates, updatedRates);

            _mockSMSService.Verify(x => x.sendMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3)); // Verify if sendMessage method is not called if no agents found

        }

        [Test]
        public async Task UpdateTaxRates_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 10, // Sample organization fee
                TaxAmount = 0,
                AgentCommission = 0,
            };
            
            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ThrowsAsync(new Exception());

            // Act & Assert
            //  var result = await _controller.UpdateTaxRates(taxRateResourceModel);

            Exception caughtException = null;
           
            try
            {
                await _controller.UpdateTaxRates(taxRateResourceModel);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);
        }

        [Test]
        public async Task UpdateAgentCommissionTaxRates_ValidModel_SuccessfullyUpdatesTaxRates()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 0, // Sample organization fee
                TaxAmount = 0,
                AgentCommission = 3,
            };

            var TransferFeeRates = new TransferFee
            {
                Id = 1,
                TaxAmount = 5.0, // Sample tax amount
                OrganizationFee = 10.0, // Sample organization fee
                AgentCommission = 3.0, // Sample agent commission
                Description = "Transfer Fee Description 1",
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            var transferType = new TransferType
            {
                Id = 1,
                Name = "Transfer Type 1",
                FromWalletType = 1, // Sample from wallet type ID
                ToWalletType = 2, // Sample to wallet type ID
                Description = "Description for Transfer Type 1",
                TransferFee = 1, // Sample transfer fee ID

            };
            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ReturnsAsync(TransferFeeRates);
            _mockTransferFeeService.Setup(x => x.AddTransferFee(It.IsAny<TransferFee>())).ReturnsAsync(TransferFeeRates);
            _mockTransferTypeService.Setup(x => x.GetTransferTypeById(It.IsAny<int>())).ReturnsAsync(transferType);
            _mockTransferTypeService.Setup(x => x.UpdateTransferTypeById(It.IsAny<TransferType>())).ReturnsAsync(transferType);
            _mockUserDetailsService.Setup(x => x.GetUserByRole(It.IsAny<int>())).ReturnsAsync(new List<ApplicationUser>());
            _mockMapper.Setup(x => x.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(new List<ApplicationUser>()));


            // Act
            var result = await _controller.SetAgentCommissionTaxRates(taxRateResourceModel);

            // Assert
            var okResult = result as OkObjectResult;
            var updatedRates = okResult.Value as TransferFee;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsInstanceOf<TransferFee>(okResult.Value);
            Assert.AreEqual(TransferFeeRates, updatedRates);

            _mockSMSService.Verify(x => x.sendMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0)); // Verify if sendMessage method is not called if no agents found

        }

        [Test]
        public async Task UpdateAgentCommissionTaxRates_OrganizationFeeZero_ReturnsBadRequest()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 0,
                TaxAmount = 0,
                AgentCommission = 0,
            };

            // Act
            var result = await _controller.SetAgentCommissionTaxRates(taxRateResourceModel);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), badRequestResult.Value);
        }

        [Test]
        public async Task UpdateAgentCommissionTaxRates_ServiceFailsToRetrieveTaxRates_ReturnsBadRequest()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 0, // Sample organization fee
                TaxAmount = 0,
                AgentCommission = 3,
            };

            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ReturnsAsync((TransferFee)null);

            // Act
            var result = await _controller.SetAgentCommissionTaxRates(taxRateResourceModel);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), badRequestResult.Value);
        }

        [Test]
        public async Task UpdateAgentCommissionTaxRates_ServiceFailsToAddTransferFee_ReturnsBadRequest()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 0, // Sample organization fee
                TaxAmount = 0,
                AgentCommission = 3,
            };

            var TransferFeeRates = new TransferFee
            {
                Id = 1,
                TaxAmount = 5.0, // Sample tax amount
                OrganizationFee = 10.0, // Sample organization fee
                AgentCommission = 3.0, // Sample agent commission
                Description = "Transfer Fee Description 1",
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ReturnsAsync(TransferFeeRates);
            _mockTransferFeeService.Setup(x => x.AddTransferFee(It.IsAny<TransferFee>())).ReturnsAsync((TransferFee)null);
            _mockMapper.Setup(x => x.Map<TaxRateResourceModel, TransferFee>(taxRateResourceModel)).Returns((TransferFee)null);

            // Act
            var result = await _controller.SetAgentCommissionTaxRates(taxRateResourceModel);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), badRequestResult.Value);
        }

        [Test]
        public async Task UpdateAgentCommissionTaxRates_ServiceFailsToRetrieveTransferType_ReturnsBadRequest()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 0, // Sample organization fee
                TaxAmount = 0,
                AgentCommission = 3,
            };

            var TransferFeeRates = new TransferFee
            {
                Id = 1,
                TaxAmount = 5.0, // Sample tax amount
                OrganizationFee = 10.0, // Sample organization fee
                AgentCommission = 3.0, // Sample agent commission
                Description = "Transfer Fee Description 1",
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ReturnsAsync(TransferFeeRates);
            _mockTransferFeeService.Setup(x => x.AddTransferFee(It.IsAny<TransferFee>())).ReturnsAsync(TransferFeeRates);
            _mockMapper.Setup(x => x.Map<TaxRateResourceModel, TransferFee>(taxRateResourceModel)).Returns(TransferFeeRates);
            _mockTransferTypeService.Setup(x => x.GetTransferTypeById(It.IsAny<int>())).ReturnsAsync((TransferType)null);


            // Act
            var result = await _controller.SetAgentCommissionTaxRates(taxRateResourceModel);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), badRequestResult.Value);
        }

        [Test]
        public async Task UpdateAgentCommissionTaxRates_ValidModelAndIfSmsServiceCalled_SuccessfullyUpdatesTaxRates()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 0, // Sample organization fee
                TaxAmount = 0,
                AgentCommission = 3,
            };

            var TransferFeeRates = new TransferFee
            {
                Id = 1,
                TaxAmount = 5.0, // Sample tax amount
                OrganizationFee = 10.0, // Sample organization fee
                AgentCommission = 3.0, // Sample agent commission
                Description = "Transfer Fee Description 1",
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            var transferType = new TransferType
            {
                Id = 1,
                Name = "Transfer Type 1",
                FromWalletType = 1, // Sample from wallet type ID
                ToWalletType = 2, // Sample to wallet type ID
                Description = "Description for Transfer Type 1",
                TransferFee = 1, // Sample transfer fee ID

            };

            IEnumerable<ApplicationUser> applicationUsers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "user1",
                    Email = "user1@example.com",
                    PhoneNumber = "1234567890",
                    UserDetail = new UserDetail {PhoneNumber = "1234567890",FirstName="john"  },

                },
                new ApplicationUser
                {
                    Id = "2",
                    UserName = "user2",
                    Email = "user2@example.com",
                    PhoneNumber = "9876543210",
                    UserDetail = new UserDetail {PhoneNumber = "9876543210", FirstName="john" },

                },
                new ApplicationUser
                {
                    Id = "3",
                    UserName = "user3",
                    Email = "user3@example.com",
                    PhoneNumber = "5555555555",
                    UserDetail = new UserDetail {PhoneNumber = "5555555555",FirstName="john"  },
                    // Add other properties as needed
              
                }
            };

            IEnumerable<ApplicationUserResource> applicationResourceUsers = new List<ApplicationUserResource>
            {
                new ApplicationUserResource
                {
                    UserName = "user1",
                    Email = "user1@example.com",
                    UserDetail = new UserDetailResource { PhoneNumber = "1234567890",FirstName="john"  },

                },
                new ApplicationUserResource
                {
                    UserName = "user2",
                    Email = "user2@example.com",
                    UserDetail = new UserDetailResource { PhoneNumber = "9876543210", FirstName="jyui"  },

                },
                new ApplicationUserResource
                {
                    UserName = "user3",
                    Email = "user3@example.com",

                    UserDetail = new UserDetailResource { PhoneNumber = "5555555555",FirstName="yuyu"  },

                }
            };

            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ReturnsAsync(TransferFeeRates);
            _mockTransferFeeService.Setup(x => x.AddTransferFee(It.IsAny<TransferFee>())).ReturnsAsync(TransferFeeRates);
            _mockTransferTypeService.Setup(x => x.GetTransferTypeById(It.IsAny<int>())).ReturnsAsync(transferType);
            _mockTransferTypeService.Setup(x => x.UpdateTransferTypeById(It.IsAny<TransferType>())).ReturnsAsync(transferType);
            _mockUserDetailsService.Setup(x => x.GetUserByRole(It.IsAny<int>())).ReturnsAsync(applicationUsers);
            _mockMapper.Setup(x => x.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(applicationUsers)).Returns(applicationResourceUsers);


            // Act
            var result = await _controller.SetAgentCommissionTaxRates(taxRateResourceModel);

            // Assert
            var okResult = result as OkObjectResult;
            var updatedRates = okResult.Value as TransferFee;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsInstanceOf<TransferFee>(okResult.Value);
            Assert.AreEqual(TransferFeeRates, updatedRates);

            _mockSMSService.Verify(x => x.sendMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3)); // Verify if sendMessage method is not called if no agents found

        }

        [Test]
        public async Task UpdateAgentCommissionTaxRates_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var taxRateResourceModel = new TaxRateResourceModel
            {
                OrganizationFee = 0, // Sample organization fee
                TaxAmount = 0,
                AgentCommission = 3,
            };

            _mockTransferFeeService.Setup(x => x.GetTransferFeeByDesc()).ThrowsAsync(new Exception());

            // Act & Assert
            //  var result = await _controller.UpdateTaxRates(taxRateResourceModel);

            Exception caughtException = null;

            try
            {
                await _controller.SetAgentCommissionTaxRates(taxRateResourceModel);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);
        }

        [Test]
        public async Task GetAllTaxRatesHistory_ValidInput_ReturnsOk()
        {
            // Arrange
            var pagingRequestForRates = new PagingRequestForRates { Index = 1, Size = 10 }; // Sample paging request
           
            IEnumerable<TransferFee> expectedTransferFees = new List<TransferFee>()
            {  
                new TransferFee
                    {
                    Id = 1,
                    TaxAmount = 5.0, // Sample tax amount
                    OrganizationFee = 10.0, // Sample organization fee
                    AgentCommission = 3.0, // Sample agent commission
                    Description = "Transfer Fee Description 1",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    },
                     new TransferFee
                    {
                    Id = 2,
                    TaxAmount = 5.0, // Sample tax amount
                    OrganizationFee = 10.0, // Sample organization fee
                    AgentCommission = 3.0, // Sample agent commission
                    Description = "Transfer Fee Description 1",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    }
            };

            var taxRateResponse = new TaxRatesResponse
            {
                TotalCount = 2,
                TaxRates = expectedTransferFees
            };

            _mockTransferFeeService.Setup(x => x.GetAllTransferFees(pagingRequestForRates.Index, pagingRequestForRates.Size)).ReturnsAsync(taxRateResponse);
            

            // Act
            var result = await _controller.GetAllTaxRatesHistory(pagingRequestForRates);

            // Assert
            var okResult = result as OkObjectResult;
            var transferFees = okResult.Value as TaxRatesResponse;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsInstanceOf<TaxRatesResponse>(okResult.Value);
            Assert.AreEqual(expectedTransferFees, transferFees.TaxRates);
        }

        [Test]
        public async Task GetAllTaxRatesHistory_ServiceFailsToRetrieveTransferFees_ReturnsInternalServerError()
        {
            // Arrange
            var pagingRequestForRates = new PagingRequestForRates { Index = 1, Size = 10 }; // Sample paging request
          
            _mockTransferFeeService.Setup(x => x.GetAllTransferFees(pagingRequestForRates.Index, pagingRequestForRates.Size)).ThrowsAsync(new Exception());

            // Act & Assert
            Exception caughtException = null;

            try
            {
                await _controller.GetAllTaxRatesHistory(pagingRequestForRates);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);
        }



    }
}
