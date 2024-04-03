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
            try
            {
                if(fromWalletTypeId == 0 || toWalletTypeId==0 || fromWalletTypeId < 0  || toWalletTypeId < 0) 
                {
                    return BadRequest("Invalid Wallet Ids.");
                }
                var transferType = await _transferTypeService.GetTransferTypeForTransaction(fromWalletTypeId, toWalletTypeId);
                var transferTypeResource = _mapper.Map<TransferType, TransferTypeResource>(transferType);
                if(transferTypeResource != null)
                {
                    return Ok(transferTypeResource);
                }
                return BadRequest("No result found");

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{typeId}")]
        public async Task<IActionResult> GetTransferType(int typeId)
        {
            try
            {
                if (typeId == 0 || typeId < 0)
                {
                    return BadRequest("Type id cannot be 0 or null");
                };

                var transferType = await _transferTypeService.GetTransferTypeById(typeId);
                var transferTypeResource = _mapper.Map<TransferType, TransferTypeResource>(transferType);

                return Ok(transferTypeResource);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

       
    }
}
