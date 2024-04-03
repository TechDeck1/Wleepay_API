using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using wleepay.Services;
using wleepay.Communication.Interface;
using wleepay.Communication.Templates;
using Twilio.TwiML.Voice;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using wleepay.API.Middleware;
using wleepay.Data.Context;
using wleepay.Core.Common;

namespace wleepay.API.Areas.Portal.Controllers
{
    [Route("api/portal/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class CashFlowHistoryController : Controller
    {
        private readonly ICashFlowService _cashFlowService;
        private readonly IMapper _mapper;
        private readonly IWalletService _walletService;
        private readonly IUserDetailService _userDetailService;
        private readonly ISMSService _sMSService;


        public CashFlowHistoryController(
            ICashFlowService cashFlowService, IMapper mapper, IWalletService walletService, IUserDetailService userDetailService, ISMSService sMSService)
        {
            _cashFlowService = cashFlowService;
            _mapper = mapper;
            _walletService = walletService;
            _userDetailService = userDetailService;
            _sMSService = sMSService;
        }

        [SystemLock]
        [HttpPost]
        public async Task<IActionResult> AddCashFlowHistoryAsync(CashFlowHistoryResource cashFlowHistoryResource)
        {
            try
            {
                if (cashFlowHistoryResource.WalletId == null || cashFlowHistoryResource.WalletId == 0)
                {
                    return BadRequest(Messages.WalletIdNull.ToDescriptionString());
                }
                
                var walletBalance = await _walletService.GetWallet((long)cashFlowHistoryResource.WalletId);
               
                if(walletBalance == null)
                {
                    return BadRequest(Messages.InvalidRequest.ToDescriptionString());
                }

                if (cashFlowHistoryResource.DepositAmount != null && cashFlowHistoryResource.DepositAmount != 0)
                {
                    cashFlowHistoryResource.TotalAmount = cashFlowHistoryResource.DepositAmount + walletBalance.Balance;

                    var cashFlowhistory = _mapper.Map<CashFlowHistoryResource, CashFlowHistory>(cashFlowHistoryResource);
                    cashFlowhistory.CurrencyId = walletBalance.CurrencyId;
                    cashFlowhistory.PreviousAmount = walletBalance.Balance;

                    var cashFlow = await _cashFlowService.AddCashFlowHistoryAsync(cashFlowhistory);
                    double amount = (double)cashFlowHistoryResource.DepositAmount;
                    var currencySymbol = cashFlowhistory.CurrencyId == 1 ? "LRD" : "USD";

                    var userDetail = await _userDetailService.GetApplicationUserById(walletBalance.UserId);

                    var message = MessagesTemplates.PaymentReceivedInCashOnHandAccount(amount.ToString("C2", new CultureInfo("en-US")), currencySymbol, userDetail?.UserDetail?.FirstName);

                    var result = _sMSService.sendMessage(message, userDetail.PhoneNumber);

                    return Ok(cashFlow);
                }
                return BadRequest(Messages.DepositAmount.ToDescriptionString());

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetCashFlowHistoryAsync([FromQuery] PagingRequestForCashFlow pagingRequestForCashFlow)
        {
            try
            {
                if(pagingRequestForCashFlow.CurrencyId == null || pagingRequestForCashFlow.CurrencyId > 2 || pagingRequestForCashFlow.CurrencyId < 0) 
                {
                    return BadRequest("currency id can only be 0 or 1 and cannot be negative");
                }
                var cashFlow = await _cashFlowService.GetAllCashFlowHistoryAsync(pagingRequestForCashFlow.CurrencyId, pagingRequestForCashFlow.Index, pagingRequestForCashFlow.Size);
                return Ok(cashFlow);

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

    }

}
