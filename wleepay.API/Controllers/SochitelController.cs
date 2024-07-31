using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using wleepay.Communication.Interface;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Storage.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using IEmailSender = wleepay.Communication.Interface.IEmailSender;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Services;
using System.Net.Http;
using System.Text;
using wleepay.Core.DTO.Request;
using wleepay.Core.Enums;
using System.Linq;
using AutoMapper.Internal;
using System;


namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class SochitelController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserDetailService _userDetailsService;
        private readonly ISMSService _smsService;
        private readonly IWalletService _walletService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailSender _emailSender;
        private readonly ISystemActivityLogsService _systemActivityLogsService;
        private readonly ISMSService _SMSService;
        private readonly IExternalApiService _externalApiService;
        private readonly ITransactionService _transactionService;


        public SochitelController(UserManager<ApplicationUser> userManager,
            IUserDetailService userDetailsService,
            IWalletService walletService,
            ILogger<AccountController> logger,
            ISMSService smsService,
            IConfiguration configuration,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment,
            IEmailSender emailSender,
            ISystemActivityLogsService systemActivityLogsService,
            ISMSService sMSService,
            IExternalApiService externalApiService,
            ITransactionService transactionService)
        {
            _userManager = userManager;
            _userDetailsService = userDetailsService;
            _walletService = walletService;
            _logger = logger;
            _smsService = smsService;
            _configuration = configuration;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _emailSender = emailSender;
            _systemActivityLogsService = systemActivityLogsService;
            _SMSService = sMSService;
            _externalApiService = externalApiService;
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("operators")]
        public async Task<IActionResult> GetOperators([FromBody] SochitelOperatorRequest sochitelOperatorRequest)
        {
            try
            {
                var result = await _externalApiService.PostDataAsync<SochitelOperatorRequest>(sochitelOperatorRequest);

                var products = JsonConvert.DeserializeObject<SochitelOperatorResponse>(result);

                List<Operator> operators = new List<Operator>();
                foreach (var kvp in products.Result)
                {
                    var product = kvp.Value;
                    operators.Add(product);
                }

                var newOperatorList = operators;

                return Ok(newOperatorList);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("operatorProducts")]
        public async Task<IActionResult> GetOperatorProducts([FromBody] SochitelProductOperatorRequest sochitelProductOperatorRequest)
        {
            try
            {
                var result = await _externalApiService.PostDataAsync<SochitelProductOperatorRequest>(sochitelProductOperatorRequest);

                var products = JsonConvert.DeserializeObject<SochitelOperatorProductsResponse>(result);

                List<Product> operatorsProduct = new List<Product>();

                foreach (var product in products.Result.Products)
                {
                    operatorsProduct.Add(product.Value);
                }

                var newOperatorProductList = operatorsProduct;

                return Ok(newOperatorProductList);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("execTransaction")]
        public async Task<IActionResult> execTransaction([FromBody]ExecTransactionRequest execTransactionRequest)
        {
            try
            {
                if (execTransactionRequest.FromWalletId != null)
                {
                    var fromWalletData = await _walletService.GetWallet(execTransactionRequest.FromWalletId);
                    if (fromWalletData == null) 
                    {
                        return BadRequest("Wallet not found.");
                    }
                    bool checkValidBalance = false;
                    if (fromWalletData.CurrencyId == (int)Currencies.LRD)
                    {
                        if (fromWalletData.Balance > execTransactionRequest.AmountInLd)
                        {
                            checkValidBalance = true;
                        }
                        else
                        {
                            checkValidBalance = false;
                            return BadRequest("There is no enough balance in wallet to make transaction");
                        }

                    }
                    if (fromWalletData.CurrencyId == (int)Currencies.USD)
                    {

                        if (fromWalletData.Balance > execTransactionRequest.AmountInUsd)
                            checkValidBalance = true;
                        else
                        {
                            checkValidBalance = false;
                            return BadRequest("There is no enough balance in wallet to make transaction");
                        }
                    }

                    if (checkValidBalance)
                    {

                        var exeTransactionToCreate = _mapper.Map<ExecTransactionRequest, SochitelTransactionRequest>(execTransactionRequest);

                        var result = await _externalApiService.PostDataAsync<SochitelTransactionRequest>(exeTransactionToCreate);

                        SochitelTransactionResponse transactionResponse = JsonConvert.DeserializeObject<SochitelTransactionResponse>(result);

                        if (transactionResponse.Status.Name == "Successful")
                        {
                            var walletData = await _walletService.GetWalletByWalletTypeId((int)WalletTypes.DebitFloatN);
                            var walletListResource = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData);
                            WalletResource walletResources = new WalletResource();
                            foreach (var walletResource in walletListResource)
                            {
                                if (walletResource != null && walletResource.CurrencyId == 2)
                                {
                                    walletResources = walletResource;
                                }
                                else
                                {
                                    walletResources = null;
                                }
                            }

                            var transactionToCreate = new Transaction()
                            {
                                ToWalletId = (long)(walletResources?.Id),
                                FromWalletId = execTransactionRequest.FromWalletId,
                                Amount = (double)execTransactionRequest.AmountInUsd,
                                TransferTypeId = (int)TransferTypes.MobileRechargeTransfer,
                                Description = "Mobile recharge transfer to system wallet",
                                CreatedDate = System.DateTime.UtcNow,
                                Status = (int)wleepay.Core.Enums.TransactionStatus.Approved,

                            };

                            var newTransaction = await _transactionService.InitiateTransaction(transactionToCreate);

                            return Ok(transactionResponse);

                        }

                        return Ok(transactionResponse);
                    }

                    
                }

                return BadRequest("Wallet Id should be valid");

            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("billPaymentTransaction")]
        public async Task<IActionResult> billPaymentTransaction([FromBody] BillPaymentTransactionRequest execTransactionRequest)
        {
            try
            {
                if (execTransactionRequest.FromWalletId != null)
                {
                    var fromWalletData = await _walletService.GetWallet(execTransactionRequest.FromWalletId);
                    if (fromWalletData == null)
                    {
                        return BadRequest("Wallet not found.");
                    }
                    bool checkValidBalance = false;
                    if(fromWalletData.CurrencyId == (int)Currencies.LRD) 
                    {
                        if (fromWalletData.Balance > execTransactionRequest.AmountInLd)
                        {
                            checkValidBalance = true;
                        }
                        else
                        {
                            checkValidBalance = false;
                            return BadRequest("There is no enough balance in wallet to make transaction");
                        }

                    }
                    if (fromWalletData.CurrencyId == (int)Currencies.USD)
                    {

                        if (fromWalletData.Balance > execTransactionRequest.AmountInUsd)
                            checkValidBalance = true;
                        else 
                        {
                            checkValidBalance = false;
                            return BadRequest("There is no enough balance in wallet to make transaction");
                        }
                    }

                    if (checkValidBalance)
                    {
                    
                        var exeTransactionToCreate = _mapper.Map<BillPaymentTransactionRequest, SochitelBillPaymentTransactionRequest>(execTransactionRequest);

                        var result = await _externalApiService.PostDataAsync<SochitelBillPaymentTransactionRequest>(exeTransactionToCreate);

                        var transactionResponse = JsonConvert.DeserializeObject<SochitelTransactionResponse>(result);

                        object TransactionResponse = transactionResponse.Result;

                        if (transactionResponse.Status.Name == "Successful")
                        {
                            var walletData = await _walletService.GetWalletByWalletTypeId((int)WalletTypes.DebitFloatN);
                            var walletListResource = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData);
                            WalletResource walletResources = new WalletResource();
                            foreach (var walletResource in walletListResource)
                            {
                                if (walletResource != null && walletResource.CurrencyId == execTransactionRequest.Currency)
                                {
                                    walletResources = walletResource;
                                }
                                else
                                {
                                    walletResources = null;
                                }
                            }
                            var transactionToCreate = new Transaction()
                            {
                                ToWalletId = (long)(walletResources?.Id),
                                FromWalletId = execTransactionRequest.FromWalletId,
                                Amount = (double)execTransactionRequest.AmountInUsd,
                                TransferTypeId = (int)TransferTypes.MobileRechargeTransfer,
                                Description = "Mobile recharge transfer to system wallet",
                                CreatedDate = System.DateTime.UtcNow,
                                Status = (int)wleepay.Core.Enums.TransactionStatus.Approved,

                            };

                            var newTransaction = await _transactionService.InitiateTransaction(transactionToCreate);
                        }

                        return Ok(TransactionResponse);

                    }
                }

                return BadRequest("Wallet Id should be valid");

            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
