using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    //[AllowAnonymous]

    public class WalletController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWalletService _walletService;
        public WalletController(IMapper mapper, IWalletService walletService)
        {
            _mapper = mapper;
            _walletService = walletService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateWallet([FromBody] AddWalletResource walletInfo)
        {
            try
            {
                var validator = new AddWalletValidator();
                var result = validator.Validate(walletInfo);
                if (!result.IsValid)
                    return BadRequest(result.Errors);

                var wallet = _mapper.Map<AddWalletResource, Wallet>(walletInfo);
                var walletData = await _walletService.AddOrUpdateWallet(wallet);

                if (walletData == null)
                    return BadRequest("No data Added/Updated for wallet");

                return Ok(wallet);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWallet(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("Please select userId.");
            
            var walletData = await _walletService.GetWalletsByUserId(userId);
            var walletListResource = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData);

            return Ok(walletListResource);
        }

        [HttpGet("{walletTypeId}/walletType")]
        public async Task<IActionResult> GetWalletByWalletTypeId(long walletTypeId)
        {
            try
            {
                if (walletTypeId == null|| walletTypeId == 0 || walletTypeId < 0)
                    return BadRequest("UserId cannot be null or 0 and negative.");

                var walletData = await _walletService.GetWalletByWalletTypeId(walletTypeId);
                var walletListResource = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData);

                return Ok(walletListResource);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
