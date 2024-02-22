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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWallet(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("Please select userId.");
            
            var walletData = await _walletService.GetWalletsByUserId(userId);
            var walletListResource = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData);

            return Ok(walletListResource);
        }

        [HttpGet("GetWalletByWalletTypeId/{walletTypeId}")]
        public async Task<IActionResult> GetWalletByWalletTypeId(long walletTypeId)
        {
            if (walletTypeId == null)
                return BadRequest("Please select userId.");

            var walletData = await _walletService.GetWalletByWalletTypeId(walletTypeId);
            var walletListResource = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData);

            return Ok(walletListResource);
        }


        [HttpPost("SetCashOnHand")]
        public async Task<IActionResult> SetCashOnHand(CashOnHandRequest request)
        {
            if (request.Amount == null)
                return BadRequest("Invalid Request");
            
            var walletData = await _walletService.SetCashOnHand(request.Amount, request.CurrencyTypeId, request.WalletTypeId);

            return Ok(walletData);
        }

    }
}
