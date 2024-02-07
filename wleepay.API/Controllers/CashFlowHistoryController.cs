using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashFlowHistoryController : Controller
    {
        private readonly ICashFlowService _cashFlowService;
        private readonly IMapper _mapper;
        private readonly IWalletService _walletService;


        public CashFlowHistoryController(
            ICashFlowService cashFlowService,IMapper mapper, IWalletService walletService)
        {

            _cashFlowService = cashFlowService;
            _mapper = mapper;
            _walletService = walletService;
        }

        [HttpPost("CashFlowHistory")]
        public async Task<IActionResult> AddCashFlowHistoryAsync(CashFlowHistoryResource cashFlowHistoryResource)
        {
            try
            {
                if (cashFlowHistoryResource.WalletId == null)
                {
                    return NotFound();
                }
                var walletBalance =await _walletService.GetWallet((long)cashFlowHistoryResource.WalletId);
                var lastCashFlowHistory = await _cashFlowService.GetLatestCashFlowHistoryAsync((long)cashFlowHistoryResource.WalletId);
                if (cashFlowHistoryResource.DepositAmount != null) 
                {
                    cashFlowHistoryResource.TotalAmount = cashFlowHistoryResource.DepositAmount + walletBalance.Balance;
                }
                else
                {
                    cashFlowHistoryResource.TotalAmount = walletBalance.Balance - cashFlowHistoryResource.WithdrawAmount;
                }

                var cashFlowhistory = _mapper.Map<CashFlowHistoryResource, CashFlowHistory>(cashFlowHistoryResource);
                cashFlowhistory.CurrencyId = walletBalance.CurrencyId;
                cashFlowhistory.PreviousAmount = walletBalance.Balance;

                var cashFlow = await _cashFlowService.AddCashFlowHistoryAsync(cashFlowhistory);
                return Ok(cashFlow);
                //return Ok();

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("GetCashFlowHistory")]
        public async Task<IActionResult> GetCashFlowHistoryAsync(PagingRequestForCashFlow pagingRequestForCashFlow )
        {
            try
            {
               
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
