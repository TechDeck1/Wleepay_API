using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Core.Models;
using wleepay.Core.Services;

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
        private readonly IUserDetailService _userDetailService;
        public WalletController(IMapper mapper, IWalletService walletService, IUserDetailService userDetailService)
        {
            _mapper = mapper;
            _walletService = walletService;
            _userDetailService = userDetailService;
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

        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetWalletByWalletTypeId(long walletTypeId, [FromQuery] string userId)
        {
            if (walletTypeId == null)
                return BadRequest("Please select userId.");

            IEnumerable<Wallet> walletData = new List<Wallet>();
            if (userId == null)
            {
               walletData = await _walletService.GetWalletByWalletTypeId(walletTypeId);
            }
            else
            {
               walletData = await _walletService.GetWalletByWalletTypeIdAndUserId(userId, walletTypeId);
            }

            var walletListResource = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData);

            return Ok(walletListResource);
        }

        [HttpGet("{userId}/{walletTypeId}/walletType")]
        public async Task<IActionResult> GetWalletByWalletTypeId(string userId, long walletTypeId)
        {
            if (walletTypeId == null || userId == null)
                return BadRequest("Please select userId.");
           
            var walletData = await _walletService.GetWalletByWalletTypeIdAndUserId(userId, walletTypeId);
            var walletListResource = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData);
            return Ok(walletListResource);
        }

    }
}
