using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.API.Controllers
{
    public class CashFlowHistoryController : Controller
    {
        private readonly ICashFlowService _cashFlowService;

        public CashFlowHistoryController(
            ICashFlowService cashFlowService)
        {

            _cashFlowService = cashFlowService;
        }

        [HttpGet("CashFlowHistory")]
        public async Task<IActionResult> AddCashFlowHistoryAsync(CashFlowHistory cashFlowHistory)
        {
            try
            {
                if(cashFlowHistory.WalletId == null) 
                {
                    return NotFound();
                }
                if(cashFlowHistory.DepositAmount != null)
                   cashFlowHistory.TotalAmount = cashFlowHistory.DepositAmount+cashFlowHistory.TotalAmount;
                else
                {
                    cashFlowHistory.TotalAmount = cashFlowHistory.TotalAmount - cashFlowHistory.WithdrawAmount;
                }
                var cashFlow = await _cashFlowService.AddCashFlowHistoryAsync(cashFlowHistory);
                return Ok(cashFlow);

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
