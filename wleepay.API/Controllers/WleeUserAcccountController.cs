using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using wleepay.Communication.Interface;
using wleepay.Core.DTO.Request;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;



namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WleeUserAcccountController : ControllerBase
    {
        private readonly IWleeSendUserDetailService _wleeSendUserDetailsService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserDetailService _userDetailsService;
        private readonly IMemoryCache _cache;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ISMSService _smsService;
        private readonly ILogger<WleeSendAccountController> _logger;
        private readonly IAddressService _addressService;
        IWleeSendUserDetailService wleeSendUserDetailsService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;


        public WleeUserAcccountController(
        IWleeSendUserDetailService wleeSendUserDetailsService,
        ILogger<WleeSendAccountController> logger,
        IAddressService addressService,
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserDetailService userDetailsService,
        IMemoryCache cache,
        IEmailSender emailSender,
        IMapper mapper,
        IAccountService accountService,
        IUserService userService,
        ISMSService smsService)
        {
            _wleeSendUserDetailsService = wleeSendUserDetailsService;
            _logger = logger;
            _addressService = addressService;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _userDetailsService = userDetailsService;
            _cache = cache;
            _emailSender = emailSender;
            _mapper = mapper;
            _accountService = accountService;
            _userService = userService;
            _smsService = smsService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest userDetail)
        {
            var response = await _accountService.RegisterAsync(userDetail);
            if (!response.Success)
                return BadRequest(response.ErrorMessage);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return BadRequest("Invalid verification request.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found.");

            var decodedToken = HttpUtility.UrlDecode(token);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
                return BadRequest(result.Errors.FirstOrDefault()?.Description ?? "Invalid verification token.");

            var userDetails = await _userDetailsService.GetUserDetailsById(userId);
            if (userDetails != null && userDetails.Status != Status.Active.GetHashCode())
            {
                userDetails.Status = Status.Active.GetHashCode();
                await _userDetailsService.AddOrUpdateUserDetails(userDetails);
            }

            return Ok("Email verified successfully.");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] EmailLoginRequest request)
        {
            var response = await _accountService.LoginAsync(request);
            if (!response.Success)
               return BadRequest(response.ErrorMessage);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("reset-pin")]
        public async Task<IActionResult> ResetPin([FromBody] ResetPinRequest request)
        {
            var userId = _userService.GetCurrentUserId();
            var response = await _accountService.ResetPinAsync(request, userId);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }


        [AllowAnonymous]
        [HttpPost("forgot-pin")]
        public async Task<IActionResult> ForgotPin([FromBody] ForgotPinRequest request)
        {
            var response = await _accountService.ForgotPinAsync(request);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

    }
}
