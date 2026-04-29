using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Core.Common;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PortalController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserDetailService _userDetailsService;
        private readonly ITransferFeeService _transferFeeService;
        private readonly ILogger<PortalController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PortalController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserDetailService userDetailsService,
            ITransferFeeService transferFeeService,
            ILogger<PortalController> logger,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userDetailsService = userDetailsService;
            _transferFeeService = transferFeeService;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] PortalLoginRequest loginRequest)
        {
            var validator = new PortalLoginRequestValidator();
            var validationResult = await validator.ValidateAsync(loginRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
                return BadRequest("No user found.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
            if (result.Succeeded)
            {
                //get user
                var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
                //generate token
                var token = GenerateJSONWebToken(userDetails);
                //Get user role
                var userRole = await _userManager.GetRolesAsync(user);
                //updated user
                UserDetail updatedUser = null;
                //set logged in status to true if first log in
                if (!userDetails.IsLoggedIn)
                    updatedUser = await _userDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, true, 0);

                return Ok(new
                {
                    UserId = user.Id,
                    Success = true,
                    Token = token,
                    IsLoggedInVerified = updatedUser != null ? updatedUser.IsLoggedIn : userDetails.IsLoggedIn,
                    Role = userRole?[0]
                });
            }
            return BadRequest("Invalid login request.");
        }

        [HttpPost("TaxRates")]
        public async Task<IActionResult> UpdateTaxRates([FromBody] TaxRateResourceModel model)
        {
            var validator = new TaxRateRequestValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var taxRates = _mapper.Map<TaxRateResourceModel, TransferFee>(model);
            var updatedRates = await _transferFeeService.UpdateTransferFee(taxRates);

            var updatedRatesResource = _mapper.Map<TransferFee, TaxRateResourceModel>(updatedRates);
            return Ok(updatedRatesResource);
        }

        #region helper methods
        private string GenerateJSONWebToken(UserDetail userInfo)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userInfo.FirstName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var token = new JwtSecurityToken(
                //issuer: _configuration["JWT:ValidIssuer"],
                //audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
