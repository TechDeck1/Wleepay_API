using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;


namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WleeTransactionController : ControllerBase
    {
        private readonly IPaymentTransactionService _transactionService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        public WleeTransactionController(IPaymentTransactionService transactionService, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _transactionService = transactionService;
            _userManager = userManager;
            _userService = userService;
        }


        [Authorize]
        [HttpGet("user-transactions")]
        public async Task<IActionResult> GetTransactionHistory([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5,
        [FromQuery] int? transferTypeId = null)
        {
            var userId = _userService.GetCurrentUserId();
            var result = await _transactionService.GetTransactionsByUserIdAsync(userId, transferTypeId, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("mobile/transactions")]
        public async Task<ActionResult<MobileTransactionPage>> GetMobileTransactions(
      [FromQuery] string kind, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _transactionService.GetMobileTransactionsAsync(
                userId, kind, page, Math.Clamp(pageSize, 1, 50));
            return Ok(result);
        }


        [Authorize(Roles = "WleeSendAdmin")]
        [HttpPost("admin/mobile-transactions")]
        public async Task<IActionResult> GetMobileTransactions([FromBody] AdminTransactionFilter filter)
        {
            var result = await _transactionService.GetTransactionsByTypeAndDateRange(
                filter.TransferTypeId, filter.StartDate, filter.EndDate, filter.PageNumber, filter.PageSize);

            return Ok(result);
        }

        [Authorize(Roles = "WleeSendAdmin")]
        [HttpPost("admin/lec-credit-transactions")]
        public async Task<IActionResult> GetLecCreditTransactions([FromBody] AdminTransactionFilter filter)
        {
            var result = await _transactionService.GetTransactionsByTypeAndDateRange(
               filter.TransferTypeId, filter.StartDate, filter.EndDate, filter.PageNumber, filter.PageSize);
            return Ok(result);
        }


        [Authorize(Roles = "WleeSendAdmin")]
        [HttpGet("admin-summary")]
        public async Task<IActionResult> GetAdminSummary()
        {
            var totalUsers = await _userManager.Users.CountAsync();
            var totalTransactions = await _transactionService.GetTransactionCountAsync();
            var totalRevenue = await _transactionService.GetTotalTransactionVolumeAsync();

            return Ok(new
            {
                totalUsers,
                totalTransactions,
                totalRevenue,
            });
        }
  

    }
}
