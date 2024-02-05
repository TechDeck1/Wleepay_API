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

        public CashFlowHistoryController(
            ICashFlowService cashFlowService,IMapper mapper)
        {

            _cashFlowService = cashFlowService;
            _mapper = mapper;
        }

        [HttpPost("CashFlowHistory")]
        public async Task<IActionResult> AddCashFlowHistoryAsync(CashFlowHistoryResource cashFlowHistory)
        {
            try
            {
                if (cashFlowHistory.WalletId == null)
                {
                    return NotFound();
                }
                if (cashFlowHistory.DepositAmount != null)
                    cashFlowHistory.TotalAmount = cashFlowHistory.DepositAmount + cashFlowHistory.TotalAmount;
                else
                {
                    cashFlowHistory.TotalAmount = cashFlowHistory.TotalAmount - cashFlowHistory.WithdrawAmount;
                }
                var walletListResource = _mapper.Map<CashFlowHistoryResource, CashFlowHistory>(cashFlowHistory);

                //var cashFlow = await _cashFlowService.AddCashFlowHistoryAsync(cashFlowHistory);
                //return Ok(cashFlow);
                return Ok();

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }

}
