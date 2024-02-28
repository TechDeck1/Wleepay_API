using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Data.Interface;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class TransferTypeController : Controller
    {
        private readonly ITransferTypeService _transferTypeService;
        private readonly IMapper _mapper;

        public TransferTypeController(ITransferTypeService transferTypeService, IMapper mapper)
        {
            _transferTypeService = transferTypeService;
            _mapper = mapper;
        }

        [HttpGet("{fromWalletTypeId}/{toWalletTypeId}")]
        public async Task<IActionResult> GetTransferType(int fromWalletTypeId, int toWalletTypeId)
        {
            var transferType = await _transferTypeService.GetTransferTypeForTransaction(fromWalletTypeId, toWalletTypeId);
            var transferTypeResource = _mapper.Map<TransferType, TransferTypeResource>(transferType);

            return Ok(transferTypeResource);
        }

        [HttpGet("{typeId}")]
        public async Task<IActionResult> GetTransferType(int typeId)
        {
            var transferType = await _transferTypeService.GetTransferTypeById(typeId);
            var transferTypeResource = _mapper.Map<TransferType, TransferTypeResource>(transferType);

            return Ok(transferTypeResource);
        }

       
    }
}
