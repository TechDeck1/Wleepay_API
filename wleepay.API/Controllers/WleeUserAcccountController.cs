using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using wleepay.API.Helpers;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Communication.Templates;
using wleepay.Core.Common;
using wleepay.Core.DTO.Request;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;



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
