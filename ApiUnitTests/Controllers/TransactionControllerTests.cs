using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
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
    public class TransactionControllerTests
    {
        private TransactionController _controller;
        private Mock<IMapper> _mockMapper;
        private Mock<IValidator<CreateTransactionResource>> _mockValidator;
        private Mock<IUserDetailService> _mockUserDetailsService;
        private Mock<ISMSService> _mockSMSService;
        private Mock<IEmailSender> _mockEmailSender;
        private Mock<ICashFlowService> _mockCashFlowService;
        private Mock<ITransactionService> _mockTransactionService;
        private Mock<IWalletService> _mockWalletService;


        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<CreateTransactionResource>>();
            _mockUserDetailsService = new Mock<IUserDetailService>();
            _mockSMSService = new Mock<ISMSService>();
            _mockEmailSender = new Mock<IEmailSender>();
            _mockWalletService = new Mock<IWalletService>();
            _mockTransactionService = new Mock<ITransactionService>();
            _mockCashFlowService = new Mock<ICashFlowService>();

            _controller = new TransactionController(
                _mockTransactionService.Object,
                _mockWalletService.Object,
                _mockSMSService.Object,
                _mockMapper.Object,
                _mockEmailSender.Object,
                _mockCashFlowService.Object,
                _mockUserDetailsService.Object

             );

        }

        [Test]
        public async Task GetTransactionHistoryByWalletId_ReturnsTransactions_WhenTransactionsExist()
        {
            // Arrange
            var request = new TransactionPageRequest { WalletId = 1, Index = 0, Size = 10 };
           
            var transactions = new List<Transaction> 
            {
                new Transaction
                {
                    Id = 1,
                    ToWalletId = 123,
                    FromWalletId = 456,
                    Amount = 100.00,
                    TransferTypeId = 1,
                    Description = "Payment",
                    CreatedDate = DateTime.Now,
                    ParentId = null,
                    Status = 1,
                   
                },
                new Transaction
                {
                    Id = 2,
                    ToWalletId = 123,
                    FromWalletId = 456,
                    Amount = 100.00,
                    TransferTypeId = 1,
                    Description = "Payment",
                    CreatedDate = DateTime.Now,
                    ParentId = null,
                    Status = 1,
                   
                },
            };
       
            var transactionResult = new TransactionResponseResource { Transactions = transactions , TotalCount = 2 };
            var transactionHistoryResource = new List<TransactionHistoryResource>() 
            {
                new TransactionHistoryResource
                {
                    Id = 1,
                    ToWalletId = 123,
                    FromWalletId = 456,
                    Amount = 100.00,
                    TransferTypeId = 1,
                    Description = "Payment",
                    CreatedDate = DateTime.Now,
                    ParentId = null,
                    Status = 1,

                },
                new TransactionHistoryResource
                {
                    Id = 2,
                    ToWalletId = 123,
                    FromWalletId = 456,
                    Amount = 100.00,
                    TransferTypeId = 1,
                    Description = "Payment",
                    CreatedDate = DateTime.Now,
                    ParentId = null,
                    Status = 1,

                }, };


            _mockTransactionService.Setup(x => x.GetTransactionsByWalletId(request.WalletId, request.Index, request.Size))
                                   .ReturnsAsync(transactionResult);
            _mockMapper.Setup(x=> x.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(transactionResult.Transactions)).Returns(transactionHistoryResource);


            // Act
            var result = await _controller.GetTrasactionHistoryByWalletId(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            IEnumerable<TransactionHistoryResource> transaction = (IEnumerable<TransactionHistoryResource>)(okResult.Value?.GetType().GetProperty("Transactions")?.GetValue(okResult.Value));
            int totalCount = (int)(okResult.Value?.GetType().GetProperty("TotalCount")?.GetValue(okResult.Value));
            Assert.AreEqual(2, totalCount);
            Assert.AreEqual(transactionHistoryResource, transaction);
        }

        [Test]
        public async Task GetTransactionHistoryByWalletId_ReturnsEmptyList_WhenNoTransactionsExist()
        {
            // Arrange
            var request = new TransactionPageRequest { WalletId = 1, Index = 0, Size = 10 };
            var transactionResult = new TransactionResponseResource { Transactions = new List<Transaction>(), TotalCount = 0 };

            var transactions = new List<Transaction> { };

            var transactionHistoryResource = new List<TransactionHistoryResource>() { };

            _mockTransactionService.Setup(x => x.GetTransactionsByWalletId(request.WalletId, request.Index, request.Size))
                                   .ReturnsAsync(transactionResult);
            _mockMapper.Setup(x => x.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(transactionResult.Transactions)).Returns(transactionHistoryResource);


            // Act
            var result = await _controller.GetTrasactionHistoryByWalletId(request);

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            var response = okObjectResult.Value as List<TransactionResource>;
            Assert.IsInstanceOf<List<TransactionResource>>(okObjectResult.Value);
            Assert.IsEmpty(response);
        }

        [Test]
        public async Task GetTransactionsByType_ReturnsTransactions_WhenTransactionsExist()
        {
            // Arrange
            var request = new TransactionReportRequest { TransferType = TransferTypes.CashOut, Index = 0, Size = 10 };
            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Id = 1,
                    ToWalletId = 123,
                    FromWalletId = 456,
                    Amount = 100.00,
                    TransferTypeId = 1,
                    Description = "Payment",
                    CreatedDate = DateTime.Now,
                    ParentId = null,
                    Status = 1,
                },
                new Transaction
                {
                    Id = 2,
                    ToWalletId = 123,
                    FromWalletId = 456,
                    Amount = 100.00,
                    TransferTypeId = 1,
                    Description = "Payment",
                    CreatedDate = DateTime.Now,
                    ParentId = null,
                    Status = 1,
                },
            };
            var transactionResult = new List<TransactionResponseResource> { new TransactionResponseResource { Transactions = transactions, TotalCount = 2 } };
            var transactionHistoryResource = new List<TransactionHistoryResource>
            {
                new TransactionHistoryResource
                {
                    Id = 1,
                    ToWalletId = 123,
                    FromWalletId = 456,
                    Amount = 100.00,
                    TransferTypeId = 1,
                    Description = "Payment",
                    CreatedDate = DateTime.Now,
                    ParentId = null,
                    Status = 1,
                },
                new TransactionHistoryResource
                {
                    Id = 2,
                    ToWalletId = 123,
                    FromWalletId = 456,
                    Amount = 100.00,
                    TransferTypeId = 1,
                    Description = "Payment",
                    CreatedDate = DateTime.Now,
                    ParentId = null,
                    Status = 1,
                },
            };
           
            _mockTransactionService.Setup(x => x.GetTransactionsByType(request.TransferType, request.Index, request.Size))
                                   .ReturnsAsync(transactions);
            _mockMapper.Setup(x => x.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(It.IsAny<IEnumerable<Transaction>>()))
                       .Returns(transactionHistoryResource);

            // Act
            var result = await _controller.GetTransactionsByType(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            var response = okResult.Value as List<TransactionHistoryResource>;
            Assert.IsInstanceOf<List<TransactionHistoryResource>>(okResult.Value);
            Assert.AreEqual(transactionHistoryResource, response);
        }

        [Test]
        public async Task GetTransactionsByType_ReturnsEmptyList_WhenNoTransactionsExist()
        {
            // Arrange
            var request = new TransactionReportRequest { TransferType = TransferTypes.CashOut, Index = 0, Size = 10 };
            var transactionResult = new List<TransactionResponseResource>(); // Empty list when no transactions exist
            var transactions = new List<Transaction> { };

            _mockTransactionService.Setup(x => x.GetTransactionsByType(request.TransferType, request.Index, request.Size))
                                   .ReturnsAsync(transactions);

            // Act
            var result = await _controller.GetTransactionsByType(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            var response = okResult.Value as List<TransactionResource>;
            Assert.IsInstanceOf<List<TransactionResource>>(okResult.Value);
            Assert.IsEmpty(response);
        }

        [Test]
        public async Task GetTransactionById_ReturnsTransaction_WhenTransactionExists()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new Transaction
            {
                Id = transactionId,
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00,
                TransferTypeId = 1,
                Description = "Payment",
                CreatedDate = DateTime.Now,
                ParentId = null,
                Status = 1,
            };
            var transactionHistoryResource = new TransactionHistoryResource
            {
                Id = transaction.Id,
                ToWalletId = transaction.ToWalletId,
                FromWalletId = transaction.FromWalletId,
                Amount = transaction.Amount,
                TransferTypeId = transaction.TransferTypeId,
                Description = transaction.Description,
                CreatedDate = transaction.CreatedDate,
                ParentId = transaction.ParentId,
                Status = transaction.Status,
            };
            _mockTransactionService.Setup(x => x.GetTransactionsById(transactionId))
                                   .ReturnsAsync(transaction);
            _mockMapper.Setup(x => x.Map<Transaction, TransactionHistoryResource>(transaction))
                       .Returns(transactionHistoryResource);

            // Act
            var result = await _controller.GetTrasactionById(transactionId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            var response = okResult.Value as TransactionHistoryResource;
            Assert.IsInstanceOf<TransactionHistoryResource>(okResult.Value);
            Assert.AreEqual(transactionHistoryResource, response);
        }

        [Test]
        public async Task GetTransactionById_ReturnsNotFound_WhenTransactionDoesNotExist()
        {
            // Arrange
            var transactionId = 1;
            Transaction transaction = new Transaction() { }; // No transaction found
            _mockTransactionService.Setup(x => x.GetTransactionsById(transactionId))
                                   .ReturnsAsync(transaction);

            // Act
            var result = await _controller.GetTrasactionById(transactionId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNull(okResult.Value);
        }

        [Test]
        public async Task GetTransactionAmount_ReturnsTransactionAmount_WhenWalletAndTransactionsExist()
        {
            // Arrange
            var userId = "user123";
            var currency = 1;
            var isMonthly = true;
            var walletId = 1;
            var transactionAmount = 500.00;
            var wallet = new Wallet { Id = walletId, Type = WalletTypes.MemberAccount.GetHashCode() };
           
            _mockWalletService.Setup(x => x.GetWalletsByUserId(userId, currency))
                              .ReturnsAsync(new List<Wallet> { wallet });
            _mockTransactionService.Setup(x => x.CalculateTotalTransactionAmount(walletId, isMonthly))
                                   .ReturnsAsync(transactionAmount);

            // Act
            var result = await _controller.GetTrasactionAmount(userId, currency, isMonthly);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(transactionAmount, (double)okResult.Value);
        }

        [Test]
        public async Task GetTransactionAmount_ReturnsBadRequest_WhenNoWalletExistsForUser()
        {
            // Arrange
            var userId = "user123";
            var currency = 1;
            var isMonthly = true;
            
            _mockWalletService.Setup(x => x.GetWalletsByUserId(userId, currency))
                              .ReturnsAsync(new List<Wallet>());

            // Act
            var result = await _controller.GetTrasactionAmount(userId, currency, isMonthly);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("No Wallet found for user", badRequestResult.Value);
        }

        [Test]
        public async Task GetTransactionAmount_ReturnsBadRequest_WhenNoWalletExistsForUser_returnsNull()
        {
            // Arrange
            var userId = "user123";
            var currency = 1;
            var isMonthly = true;

            _mockWalletService.Setup(x => x.GetWalletsByUserId(userId, currency))
                              .ReturnsAsync((List<Wallet>)null);

            // Act
            var result = await _controller.GetTrasactionAmount(userId, currency, isMonthly);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("No Wallet found for user", badRequestResult.Value);
        }

        [Test]
        public async Task CashoutTransactions_ReturnsTransactions_WhenValidResultExists()
        {
            // Arrange
            var pageIndex = 0;
            var pageSize = 10;
            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, /* other properties */ },
                new Transaction { Id = 2, /* other properties */ }
            };
           
            var result = new TransactionResponseResource { Transactions = transactions, TotalCount = 2 };
            var transactionHistoryResources = new List<TransactionHistoryResource> { /* Map properties accordingly */ }; ;

            _mockTransactionService.Setup(x => x.GetCashoutTransactions(pageIndex, pageSize)).ReturnsAsync(result);
            _mockMapper.Setup(x => x.Map<Transaction, TransactionHistoryResource>(new Transaction())).Returns(new TransactionHistoryResource());

            // Act
            var response = await _controller.CashoutTransactions(pageIndex, pageSize);

            // Assert
            var okResult = response as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(response);
            List<TransactionHistoryResource> transaction = (List<TransactionHistoryResource>)(okResult.Value?.GetType().GetProperty("Transactions")?.GetValue(okResult.Value));
            int totalCount = (int)(okResult.Value?.GetType().GetProperty("TotalCount")?.GetValue(okResult.Value));
            Assert.AreEqual(2, totalCount);
            //Assert.AreEqual(transactionHistoryResources, transaction);
        }

        [Test]
        public async Task CashoutTransactions_ReturnsBadRequest_WhenResultIsNull()
        {
            // Arrange
            var pageIndex = 0;
            var pageSize = 10;
            var transactions = new List<Transaction>() { };

            var result = new TransactionResponseResource { Transactions = transactions, TotalCount = 0 };
            
            _mockTransactionService.Setup(x => x.GetCashoutTransactions(pageIndex, pageSize)).ReturnsAsync(result);

            // Act
            var response = await _controller.CashoutTransactions(pageIndex, pageSize);

            // Assert
            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
           
        }

        [Test]
        public async Task TopUpTransactions_ReturnsTransactions_WhenValidResultExists()
        {
            // Arrange
            var pageIndex = 0;
            var pageSize = 10;
            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, /* other properties */ },
                new Transaction { Id = 2, /* other properties */ }
            };

            var result = new TransactionResponseResource { Transactions = transactions, TotalCount = 2 };
            var transactionHistoryResources = new List<TransactionHistoryResource> { /* Map properties accordingly */ }; ;

            _mockTransactionService.Setup(x => x.GetTopUpTransactions(pageIndex, pageSize)).ReturnsAsync(result);
            _mockMapper.Setup(x => x.Map<Transaction, TransactionHistoryResource>(new Transaction())).Returns(new TransactionHistoryResource());

            // Act
            var response = await _controller.TopUpTransactions(pageIndex, pageSize);

            // Assert
            var okResult = response as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(response);
            List<TransactionHistoryResource> transaction = (List<TransactionHistoryResource>)(okResult.Value?.GetType().GetProperty("Transactions")?.GetValue(okResult.Value));
            int totalCount = (int)(okResult.Value?.GetType().GetProperty("TotalCount")?.GetValue(okResult.Value));
            Assert.AreEqual(2, totalCount);
            //Assert.AreEqual(transactionHistoryResources, transaction);
        }

        [Test]
        public async Task TopUpTransactions_ReturnsBadRequest_WhenResultIsNull()
        {
            // Arrange
            var pageIndex = 0;
            var pageSize = 10;
            var transactions = new List<Transaction>() { };

            var result = new TransactionResponseResource { Transactions = transactions, TotalCount = 0 };

            _mockTransactionService.Setup(x => x.GetTopUpTransactions(pageIndex, pageSize)).ReturnsAsync(result);

            // Act
            var response = await _controller.TopUpTransactions(pageIndex, pageSize);

            // Assert
            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(response);

        }

        [Test]
        public async Task GetTransactionByTypeAndCurrency_ReturnsTransactions_WhenValidResultExists()
        {
            // Arrange
            int transferTypeId = 1;
            int currencyTypeId = 1;
            int pageIndex = 0;
            int pageSize = 10;
            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, /* other properties */ },
                new Transaction { Id = 2, /* other properties */ }
            };
            var result = new TransactionResponseResource { Transactions = transactions, TotalCount = 2 };
            var transactionHistoryResources = new TransactionHistoryResource { /* Map properties accordingly */ };
            _mockTransactionService.Setup(x => x.GetTransactionsByTypeAndCurrency(transferTypeId, currencyTypeId, pageIndex, pageSize)).ReturnsAsync(result);
            _mockMapper.Setup(x => x.Map<Transaction, TransactionHistoryResource>(new Transaction())).Returns(transactionHistoryResources);

            // Act
            var response = await _controller.GetTransactionByTypeAndCurrency(transferTypeId, currencyTypeId, pageIndex, pageSize);

            // Assert
            var okResult = response as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(response);
            List<TransactionHistoryResource> transaction = (List<TransactionHistoryResource>)(okResult.Value?.GetType().GetProperty("Transactions")?.GetValue(okResult.Value));
            int totalCount = (int)(okResult.Value?.GetType().GetProperty("TotalCount")?.GetValue(okResult.Value));
            Assert.AreEqual(2, totalCount);

        }

        [Test]
        public async Task GetTransactionByTypeAndCurrency_ReturnsBadRequest_WhenResultIsNull()
        {
            // Arrange
            int transferTypeId = 1;
            int currencyTypeId = 1;
            int pageIndex = 0;
            int pageSize = 10;
            List<Transaction> transaction = new List<Transaction>() { };
            var result = new TransactionResponseResource { Transactions = transaction, TotalCount = 2 };

            _mockTransactionService.Setup(x => x.GetTransactionsByTypeAndCurrency(transferTypeId, currencyTypeId, pageIndex, pageSize)).ReturnsAsync(result);

            // Act
            var response = await _controller.GetTransactionByTypeAndCurrency(transferTypeId, currencyTypeId, pageIndex, pageSize);

            // Assert
            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual("Invalid UserId", badRequestResult.Value);
        }

        [Test]
        public async Task CreateTransaction_ReturnsOk_WhenValidationPassesAndTransactionCreated()
        {
            // Arrange
            var transactionRequest = new CreateTransactionResource()
            {
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00f,
                TransferTypeId = 1,
                ParentId = null,
                Description = "Payment"
            };

            // Valid request
            var validationResult = new ValidationResult(); // No validation errors
            _mockValidator.Setup(x => x.ValidateAsync(transactionRequest, default)).ReturnsAsync(validationResult);

            var walletBalance = new Wallet()
            {
                Id = 1,
                UserId = "user123",
                CurrencyId = 1,
                Type = 1,
                Balance = 1000.00,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                TransactionFrom = new List<Transaction>
                {
                    new Transaction
                    {
                        Id = 1,
                        ToWalletId = 1,
                        FromWalletId = 2,
                        Amount = 50.00,
                        TransferTypeId = 1,
                        Description = "Payment",
                        CreatedDate = DateTime.UtcNow,
                        ParentId = null,
                        Status = 1
                    },
                },
                TransactionTo = new List<Transaction>
                {
                     new Transaction
                    {
                        Id = 2,
                        ToWalletId = 1,
                        FromWalletId = 3,
                        Amount = 75.00,
                        TransferTypeId = 2,
                        Description = "Withdrawal",
                        CreatedDate = DateTime.UtcNow,
                        ParentId = null,
                        Status = 1
                    }
                },
                ApplicationUser = new ApplicationUser()
                {
                    Email="example@xem.com",
                    PhoneNumber ="123467787",
                    UserDetail = new UserDetail() { PhoneNumber= "123467787" , FirstName="john", LastName="tom" }
                },
                Currency = new Currency() { Id=1, Name = "LD" , Symbol= "L$" }


            }; 
           
            _mockWalletService.Setup(x => x.GetWallet(transactionRequest.FromWalletId)).ReturnsAsync(walletBalance);
          
            _mockWalletService.Setup(x => x.GetUserDetailsByWalletIdAndCurrency(transactionRequest.FromWalletId)).ReturnsAsync(walletBalance);
            _mockWalletService.Setup(x => x.GetUserDetailsByWalletIdAndCurrency(transactionRequest.ToWalletId)).ReturnsAsync(walletBalance);

            var createdTransaction = new Transaction() 
            {
                Id = 1,
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00f,
                TransferTypeId = 1,
                ParentId = null,
                Description = "Payment",
                Status = (int)wleepay.Core.Enums.TransactionStatus.Pending
            }; 
            
            _mockTransactionService.Setup(x => x.InitiateTransaction(It.IsAny<Transaction>())).ReturnsAsync(createdTransaction);

            var mappedTransactionResource = new TransactionResource()
            {
                Id = 1,
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00f,
                TransferTypeId = 1,
                Description = "Payment",
                CreatedDate = DateTime.UtcNow,
                ParentId = null,
                Status = 1,
                TransferType = new TransferTypeResource { Id = 1, Name = "TransferType" },
                TransactionStatus = new LookupResource { Id = 1, Name = "Status" },
                Currency = new CurrencyResource { Id = 1, Name = "Currency" }
            };
          
            _mockMapper.Setup(x => x.Map<CreateTransactionResource, Transaction>(transactionRequest)).Returns(createdTransaction);

            _mockMapper.Setup(x => x.Map<Transaction, TransactionResource>(createdTransaction)).Returns(mappedTransactionResource);
            _mockSMSService.Setup(x => x.sendMessage("", "")).Returns("queued");
            _mockEmailSender.Setup(x => x.SendEmailAsync("", "", "", "")).ReturnsAsync("queued");



            // Act
            var response = await _controller.CreateTransaction(transactionRequest);

            // Assert
            var okResult = response as OkObjectResult; 
            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.AreEqual(mappedTransactionResource, okResult.Value);
        }

        [Test]
        public async Task CreateTransaction_AddsCashFlowForWithdraw_WhenTransferTypeIdIsTopUpSystemWallet()
        {
            // Arrange
            var transactionRequest = new CreateTransactionResource()
            {
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00f,
                TransferTypeId = TransferTypes.TopUpSystemWallet.GetHashCode(),
                ParentId = null,
                Description = "Payment"
            };

            var validationResult = new ValidationResult(); // No validation errors
            _mockValidator.Setup(x => x.ValidateAsync(transactionRequest, default)).ReturnsAsync(validationResult);

            var walletBalance = new Wallet()
            {
                Id = 1,
                UserId = "user123",
                CurrencyId = 1,
                Type = 1,
                Balance = 1000.00,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                TransactionFrom = new List<Transaction>
                {
                    new Transaction
                    {
                        Id = 1,
                        ToWalletId = 1,
                        FromWalletId = 2,
                        Amount = 50.00,
                        TransferTypeId = TransferTypes.TopUpSystemWallet.GetHashCode(),
                        Description = "Payment",
                        CreatedDate = DateTime.UtcNow,
                        ParentId = null,
                        Status = 1
                    },
                },
                TransactionTo = new List<Transaction>
                {
                    new Transaction
                    {
                        Id = 2,
                        ToWalletId = 1,
                        FromWalletId = 3,
                        Amount = 75.00,
                        TransferTypeId = TransferTypes.TopUpSystemWallet.GetHashCode(),
                        Description = "Withdrawal",
                        CreatedDate = DateTime.UtcNow,
                        ParentId = null,
                        Status = 1
                    }
                },
                ApplicationUser = new ApplicationUser()
                {
                    Id = "user1234",
                    Email = "example@xem.com",
                    PhoneNumber = "123467787",
                    UserDetail = new UserDetail() { PhoneNumber = "123467787", FirstName = "john", LastName = "tom" }
                },
                Currency = new Currency() { Id = 1, Name = "LD", Symbol = "L$" }


            };

            _mockWalletService.Setup(x => x.GetWallet(transactionRequest.FromWalletId)).ReturnsAsync(walletBalance);

            _mockWalletService.Setup(x => x.GetUserDetailsByWalletIdAndCurrency(transactionRequest.FromWalletId)).ReturnsAsync(walletBalance);
            _mockWalletService.Setup(x => x.GetUserDetailsByWalletIdAndCurrency(transactionRequest.ToWalletId)).ReturnsAsync(walletBalance);

            var createdTransaction = new Transaction()
            {
                Id = 1,
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00f,
                TransferTypeId = TransferTypes.TopUpSystemWallet.GetHashCode(),
                ParentId = null,
                Description = "Payment",
                Status = (int)wleepay.Core.Enums.TransactionStatus.Pending
            };

            _mockTransactionService.Setup(x => x.InitiateTransaction(It.IsAny<Transaction>())).ReturnsAsync(createdTransaction);

            var mappedTransactionResource = new TransactionResource()
            {
                Id = 1,
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00f,
                TransferTypeId = TransferTypes.TopUpSystemWallet.GetHashCode(),
                Description = "Payment",
                CreatedDate = DateTime.UtcNow,
                ParentId = null,
                Status = 1,
                TransferType = new TransferTypeResource { Id = 1, Name = "TransferType" },
                TransactionStatus = new LookupResource { Id = 1, Name = "Status" },
                Currency = new CurrencyResource { Id = 1, Name = "Currency" }
            };

            var userDetailsResponse = new UserDetailsResponseResource
            {
                TotalCount = 2, // Example total count
                UserDetails = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Id = "1",
                        UserName = "user1",
                        Email = "user1@example.com",
                        // Add more properties as needed
                    },
                    new ApplicationUser
                    {
                        Id = "2",
                        UserName = "user2",
                        Email = "user2@example.com",
                        // Add more properties as needed
                    }
                    // Add more user details as needed
                }
            };


            _mockMapper.Setup(x => x.Map<CreateTransactionResource, Transaction>(transactionRequest)).Returns(createdTransaction);

            _mockMapper.Setup(x => x.Map<Transaction, TransactionResource>(createdTransaction)).Returns(mappedTransactionResource);
            _mockSMSService.Setup(x => x.sendMessage("", "")).Returns("queued");
            _mockEmailSender.Setup(x => x.SendEmailAsync("", "", "", "")).ReturnsAsync("queued");
            _mockMapper.Setup(x => x.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(new List<ApplicationUser>()));
            _mockUserDetailsService.Setup(x => x.GetAllUsersById(Role.Agent, 0, 0, walletBalance.ApplicationUser.Id)).ReturnsAsync(userDetailsResponse);


            // Act
            var response = await _controller.CreateTransaction(transactionRequest);

            // Assert
            _mockCashFlowService.Verify(x => x.AddCashFlowHistoryForWithdraw(It.IsAny<CashFlowHistory>()), Times.Once);
        }

        [Test]
        public async Task CreateTransaction_ReturnsBadRequest_WhenServiceNotRetriveWalletBalance()
        {
            // Arrange
            var transactionRequest = new CreateTransactionResource()
            {
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00f,
                TransferTypeId = TransferTypes.TopUpSystemWallet.GetHashCode(),
                ParentId = null,
                Description = "Payment"
            };

            var validationResult = new ValidationResult(); // No validation errors
            _mockValidator.Setup(x => x.ValidateAsync(transactionRequest, default)).ReturnsAsync(validationResult);

            var walletBalance = new Wallet(); // Existing wallet
            _mockWalletService.Setup(x => x.GetWallet(transactionRequest.FromWalletId)).ReturnsAsync((Wallet)null);

            var createdTransaction = new Transaction(); // Newly created transaction
            _mockTransactionService.Setup(x => x.InitiateTransaction(It.IsAny<Transaction>())).ReturnsAsync(createdTransaction);

            // Act
            var response = await _controller.CreateTransaction(transactionRequest);

            // Assert
            var okResult = response as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual(Messages.NotFound.ToDescriptionString(), okResult.Value);
        }

        [Test]
        public async Task CreateTransaction_UnhandeledException_ReturnsBadRequest()
        {
            // Arrange
            var transactionRequest = new CreateTransactionResource()
            {
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00f,
                TransferTypeId = TransferTypes.TopUpSystemWallet.GetHashCode(),
                ParentId = null,
                Description = "Payment"
            };
            var validationResult = new ValidationResult(); // No validation errors
            _mockValidator.Setup(x => x.ValidateAsync(transactionRequest, default)).ReturnsAsync(validationResult);

            var walletBalance = new Wallet(); // Existing wallet
            _mockWalletService.Setup(x => x.GetWallet(transactionRequest.FromWalletId)).ReturnsAsync(walletBalance);

            var createdTransaction = new Transaction(); // Newly created transaction
           
            _mockTransactionService.Setup(x => x.InitiateTransaction(It.IsAny<Transaction>())).ThrowsAsync(new Exception());

            // Act

            Exception caughtException = null;

            try
            {
                await _controller.CreateTransaction(transactionRequest);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);
            
        }

        [Test]
        public async Task CreatePaymentRequest_ReturnsOk_WhenValidationPassesAndTransactionCreated()
        {
            // Arrange
            var transactionRequest = new CreateTransactionResource()
            {
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00f,
                TransferTypeId = 1,
                ParentId = null,
                Description = "Payment",
            };
            
             var validationResult = new ValidationResult(); // No validation errors
            _mockValidator.Setup(x => x.ValidateAsync(transactionRequest, default)).ReturnsAsync(validationResult);

            var newTransaction = new Transaction() {
                Id=1,
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00f,
                TransferTypeId = 1,
                ParentId = null,
                Description = "Payment",
                Status = (int)wleepay.Core.Enums.TransactionStatus.Pending,
            };

            var walletBalance = new Wallet()
            {
                Id = 1,
                UserId = "user123",
                CurrencyId = 1,
                Type = 1,
                Balance = 1000.00,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                TransactionFrom = new List<Transaction>
                {
                    new Transaction
                    {
                        Id = 1,
                        ToWalletId = 1,
                        FromWalletId = 2,
                        Amount = 50.00,
                        TransferTypeId = 1,
                        Description = "Payment",
                        CreatedDate = DateTime.UtcNow,
                        ParentId = null,
                        Status = 1
                    },
                },
                TransactionTo = new List<Transaction>
                {
                     new Transaction
                    {
                        Id = 2,
                        ToWalletId = 1,
                        FromWalletId = 3,
                        Amount = 75.00,
                        TransferTypeId = 2,
                        Description = "Withdrawal",
                        CreatedDate = DateTime.UtcNow,
                        ParentId = null,
                        Status = 1
                    }
                },
                ApplicationUser = new ApplicationUser()
                {
                    Email = "example@xem.com",
                    PhoneNumber = "123467787",
                    UserDetail = new UserDetail() { PhoneNumber = "123467787", FirstName = "john", LastName = "tom" }
                },
                Currency = new Currency() { Id = 1, Name = "LD", Symbol = "L$" }


            };

            _mockWalletService.Setup(x => x.GetWallet(transactionRequest.FromWalletId)).ReturnsAsync(walletBalance);

            _mockWalletService.Setup(x => x.GetUserDetailsByWalletIdAndCurrency(transactionRequest.FromWalletId)).ReturnsAsync(walletBalance);
            _mockWalletService.Setup(x => x.GetUserDetailsByWalletIdAndCurrency(transactionRequest.ToWalletId)).ReturnsAsync(walletBalance);


            _mockTransactionService.Setup(x => x.InitiateTransactionRequest(It.IsAny<Transaction>())).ReturnsAsync(newTransaction);

            var mappedTransactionResource = new TransactionResource() { ToWalletId = 123, FromWalletId = 456, Amount = 100.00f, TransferTypeId = 1, ParentId = null, Description = "Payment", Status= (int)wleepay.Core.Enums.TransactionStatus.Pending, }; // Mapped transaction resource
            _mockMapper.Setup(x => x.Map<Transaction, TransactionResource>(newTransaction)).Returns(mappedTransactionResource);

            // Act
            var response = await _controller.CreatePaymentRequest(transactionRequest);

            // Assert
            var okResult = response as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.AreEqual(mappedTransactionResource, okResult.Value);
        }

        [Test]
        public async Task CreatePaymentRequest_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var transactionRequest = new CreateTransactionResource()
            {
                ToWalletId = 123,
                FromWalletId = 456,
                Amount = 100.00f,
                TransferTypeId = TransferTypes.TopUpSystemWallet.GetHashCode(),
                ParentId = null,
                Description = "Payment"
            };

            var validationResult = new ValidationResult(); // No validation errors
            _mockValidator.Setup(x => x.ValidateAsync(transactionRequest, default)).ReturnsAsync(validationResult);

            var createdTransaction = new Transaction(); // Newly created transaction

            _mockTransactionService.Setup(x => x.InitiateTransactionRequest(It.IsAny<Transaction>())).ThrowsAsync(new Exception());

            // Act

            Exception caughtException = null;

            try
            {
                await _controller.CreatePaymentRequest(transactionRequest);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);
        }

        [Test]
        public async Task CancelPaymentRequest_ReturnsBadRequest_WhenPaymentRequestIdIsInvalid()
        {
            // Arrange
            long invalidPaymentRequestId = 0;

            // Act
            var response = await _controller.CancelPaymentRequest(invalidPaymentRequestId);

            // Assert
            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual("Please enter valid payment request Id", badRequestResult.Value);
        }

        [Test]
        public async Task CancelPaymentRequest_ReturnsBadRequest_WhenTransactionStatusChangeFails()
        {
            // Arrange
            long validPaymentRequestId = 123;

            var transactionToApprove = new Transaction() { }; // Mocked transaction
            _mockTransactionService.Setup(x => x.GetTransactionsById(validPaymentRequestId)).ReturnsAsync(transactionToApprove);

            var updatedTransaction = new Transaction() { }; // Mocked updated transaction
            _mockTransactionService.Setup(x => x.UpdateTransactionStatus(validPaymentRequestId, wleepay.Core.Enums.TransactionStatus.Cancel)).ReturnsAsync((Transaction)null);

            var mappedTransactionResource = new TransactionResource() { }; // Mocked mapped transaction resource
            _mockMapper.Setup(x => x.Map<Transaction, TransactionResource>(updatedTransaction)).Returns(mappedTransactionResource);


            // Act
            var response = await _controller.CancelPaymentRequest(validPaymentRequestId);

            // Assert
            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual("Invalid payment Id", badRequestResult.Value);
        }

        [Test]
        public async Task CancelPaymentRequest_ReturnsOk_WhenPaymentRequestIsCancelledSuccessfully()
        {
            // Arrange
            long validPaymentRequestId = 123;
            var expectedResult = new object(); // Example result
            var transactionToApprove = new Transaction() { }; // Mocked transaction
            _mockTransactionService.Setup(x => x.GetTransactionsById(validPaymentRequestId)).ReturnsAsync(transactionToApprove);

            var updatedTransaction = new Transaction() { }; // Mocked updated transaction
            _mockTransactionService.Setup(x => x.UpdateTransactionStatus(validPaymentRequestId, wleepay.Core.Enums.TransactionStatus.Cancel)).ReturnsAsync(updatedTransaction);

            var mappedTransactionResource = new TransactionResource() { }; // Mocked mapped transaction resource
            _mockMapper.Setup(x => x.Map<Transaction, TransactionResource>(updatedTransaction)).Returns(mappedTransactionResource);


            // Act
            var response = await _controller.CancelPaymentRequest(validPaymentRequestId);

            // Assert
            var okResult = response as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.AreEqual(mappedTransactionResource, okResult.Value);
        }


        [Test]
        public async Task RejectPaymentRequest_ReturnsBadRequest_WhenPaymentRequestIdIsInvalid()
        {
            // Arrange
            long invalidPaymentRequestId = 0;

            // Act
            var response = await _controller.RejectPaymentRequest(invalidPaymentRequestId);

            // Assert
            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual("Please enter valid payment request Id", badRequestResult.Value);
        }

        [Test]
        public async Task RejectPaymentRequest_ReturnsBadRequest_WhenTransactionToRejectIsNotFound()
        {
            // Arrange
            long validPaymentRequestId = 123;
            _mockTransactionService.Setup(x => x.GetTransactionsById(validPaymentRequestId)).ReturnsAsync((Transaction)null); // Simulating transaction not found

            // Act
            var response = await _controller.RejectPaymentRequest(validPaymentRequestId);

            // Assert
            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual("Invalid payment Id", badRequestResult.Value);
        }

        [Test]
        public async Task RejectPaymentRequest_ReturnsBadRequest_WhenTransactionStatusChangeFails()
        {
            // Arrange
            long validPaymentRequestId = 123;
            var transactionToReject = new Transaction(); // Valid transaction to reject
            _mockTransactionService.Setup(x => x.GetTransactionsById(validPaymentRequestId)).ReturnsAsync(transactionToReject);

            var transactionToApprove = new Transaction() { }; // Mocked transaction
            _mockTransactionService.Setup(x => x.GetTransactionsById(validPaymentRequestId)).ReturnsAsync(transactionToApprove);

            var updatedTransaction = new Transaction() { }; // Mocked updated transaction
            _mockTransactionService.Setup(x => x.UpdateTransactionStatus(validPaymentRequestId, wleepay.Core.Enums.TransactionStatus.Cancel)).ReturnsAsync(updatedTransaction);

            var mappedTransactionResource = new TransactionResource() { }; // Mocked mapped transaction resource
            _mockMapper.Setup(x => x.Map<Transaction, TransactionResource>(updatedTransaction)).Returns(mappedTransactionResource);


            // Act
            var response = await _controller.RejectPaymentRequest(validPaymentRequestId);

            // Assert
            var badRequestResult = response as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual("Invalid payment Id", badRequestResult.Value);
        }

        [Test]
        public async Task RejectPaymentRequest_ReturnsOk_WhenPaymentRequestIsRejectedSuccessfully()
        {
            // Arrange
            long validPaymentRequestId = 123;
            var transactionToReject = new Transaction(); // Valid transaction to reject
            _mockTransactionService.Setup(x => x.GetTransactionsById(validPaymentRequestId)).ReturnsAsync(transactionToReject);

            var expectedResult = new object(); // Example result

            var transactionToApprove = new Transaction() { }; // Mocked transaction
            _mockTransactionService.Setup(x => x.GetTransactionsById(validPaymentRequestId)).ReturnsAsync(transactionToApprove);

            var updatedTransaction = new Transaction() { }; // Mocked updated transaction
            _mockTransactionService.Setup(x => x.UpdateTransactionStatus(validPaymentRequestId, wleepay.Core.Enums.TransactionStatus.Cancel)).ReturnsAsync(updatedTransaction);

            var mappedTransactionResource = new TransactionResource() { }; // Mocked mapped transaction resource
            _mockMapper.Setup(x => x.Map<Transaction, TransactionResource>(updatedTransaction)).Returns(mappedTransactionResource);



            // Act
            var response = await _controller.RejectPaymentRequest(validPaymentRequestId);

            // Assert
            var okResult = response as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.AreEqual(expectedResult, okResult.Value);
        }

    }
}
