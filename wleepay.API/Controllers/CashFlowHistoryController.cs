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
                var lastCashFlowHistory = await _cashFlowService.GetLatestCashFlowHistoryAsync();
                if (cashFlowHistoryResource.DepositAmount != null)
                    cashFlowHistoryResource.TotalAmount = cashFlowHistoryResource.DepositAmount + lastCashFlowHistory.TotalAmount;
                else
                {
                    cashFlowHistoryResource.TotalAmount = lastCashFlowHistory.TotalAmount - cashFlowHistoryResource.WithdrawAmount;
                }
                var cashFlowhistory = _mapper.Map<CashFlowHistoryResource, CashFlowHistory>(cashFlowHistoryResource);

                var cashFlow = await _cashFlowService.AddCashFlowHistoryAsync(cashFlowhistory);
                return Ok(cashFlow);
                //return Ok();

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetCashFlowHistory")]
        public async Task<IActionResult> GetCashFlowHistoryAsync()
        {
            try
            {
               
                var cashFlow = await _cashFlowService.GetAllCashFlowHistoryAsync();
                return Ok(cashFlow);

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }

}
