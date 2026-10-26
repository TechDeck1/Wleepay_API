using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponCodeService _couponService;
        private readonly ISystemActivityLogsService _activityLogService;
        private readonly ILogger<CouponController> _logger;

        public CouponController(
            ICouponCodeService couponService,
            ISystemActivityLogsService activityLogService,
            ILogger<CouponController> logger)
        {
            _couponService = couponService;
            _activityLogService = activityLogService;
            _logger = logger;
        }

        [HttpPost("CreateCouponCode")]
        public async Task<IActionResult> CreateCouponCode([FromBody] CouponReqDto couponReqDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _couponService.CreateCouponAsync(couponReqDto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await LogFailureAsync("Create Coupon Failed", $"Failed to create coupon code. Error: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("GetAllCoupons")]
        public async Task<IActionResult> GetAllCoupons([FromQuery] CouponListQueryDto query)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _couponService.GetAllCouponsAsync(query);

                if (result == null)
                    return NotFound(new { message = "No coupons found." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await LogFailureAsync("Get Coupons Failed", $"Failed to retrieve coupons. Error: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }



        [HttpGet("visible-coupons")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllVisibleCoupons()
        {
            try
            {
                var coupons = await _couponService.GetVisibleCouponsAsync();
                return Ok(coupons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await LogFailureAsync("Get Visible Coupons Failed", $"Failed to retrieve visible coupons. Error: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPatch("update-visibility")]
        public async Task<IActionResult> UpdateVisibility([FromBody] UpdateCouponVisibilityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _couponService.UpdateVisibilityAsync(dto.CouponCode, dto.Status!.Value);

                if (!updated)
                    return NotFound(new { message = $"Coupon '{dto.CouponCode}' not found." });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await LogFailureAsync("Update Coupon Visibility Failed", $"Failed to update visibility for coupon '{dto?.CouponCode}'. Error: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpDelete("{couponCode}")]
        public async Task<IActionResult> DeleteCoupon(string couponCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(couponCode))
                    return BadRequest(new { message = "Coupon code cannot be empty." });

                var deleted = await _couponService.DeleteCouponAsync(couponCode);

                if (!deleted)
                    return NotFound(new { message = $"Coupon '{couponCode}' not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await LogFailureAsync("Delete Coupon Failed", $"Failed to delete coupon '{couponCode}'. Error: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        private async Task LogFailureAsync(string action, string description)
        {
            try
            {
                await _activityLogService.AddSystemLogs(new SystemActivityLog
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Action = action,
                    Description = description,
                    CreatedDate = DateTime.UtcNow
                });
            }
            catch (Exception logEx)
            {
                _logger.LogError(logEx, "Failed to write system activity log.");
            }
        }
    }
}