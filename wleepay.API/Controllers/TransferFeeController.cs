using AutoMapper;
using DevExpress.Data.ODataLinq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Core.Common;
using wleepay.Core.Models;
using wleepay.Core.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using wleepay.Communication.Interface;
using DevExpress.Data.Filtering.Helpers;
using wleepay.Core.Enums;
using Microsoft.SqlServer.Management.Smo;
using wleepay.API.Helpers;
using System.Text.Json;
using wleepay.Communication.Services;
using wleepay.Communication.Templates;
using wleepay.API.Middleware;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class TransferFeeController : Controller
    {

        private readonly IUserDetailService _userDetailsService;
        private readonly ITransferFeeService _transferFeeService;
        private readonly IMapper _mapper;
        private readonly ISMSService _SMSService;
        private readonly ITransferTypeService _transferTypeService;

        public TransferFeeController(
            IUserDetailService userDetailsService,
            ITransferFeeService transferFeeService,
            IMapper mapper,
            ISMSService sMSService,
            ITransferTypeService transferTypeService
            )
        {

            _userDetailsService = userDetailsService;
            _transferFeeService = transferFeeService;
            _mapper = mapper;
            _SMSService = sMSService;
            _transferTypeService = transferTypeService;
        }

      
        [SystemLock]
        [HttpPost("SetCashOutTaxRate")]
        public async Task<IActionResult> UpdateTaxRates([FromBody] TaxRateResourceModel taxRateResourceModel)
        {
            try
            {
                if (taxRateResourceModel.OrganizationFee.Equals(0)) return BadRequest(Messages.InvalidRequest.ToDescriptionString());

                var taxRates = await _transferFeeService.GetTransferFeeByDesc();
                taxRateResourceModel.AgentCommission = taxRates.AgentCommission;
                taxRateResourceModel.OrganizationFee = taxRateResourceModel.OrganizationFee;
                taxRateResourceModel.TaxAmount = taxRates.TaxAmount;
                taxRateResourceModel.Description = taxRates.Description;

                var updatedTaxRatesResource = _mapper.Map<TaxRateResourceModel, TransferFee>(taxRateResourceModel);
                var updatedRates = await _transferFeeService.AddTransferFee(updatedTaxRatesResource);
                if (updatedRates != null)
                {
                    var types = await _transferTypeService.GetTransferTypeById((int)TransferTypes.CashOut);
                    types.TransferFee = updatedRates.Id;
                    await _transferTypeService.UpdateTransferTypeById(types);

                    var allAgents = await _userDetailsService.GetAllUsersByRole((int)Role.Agent);
                    var agentsListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(allAgents);

                    foreach (var agent in agentsListResource)
                    {
                        var message = MessagesTemplates.TaxRateUpdateMessage(taxRateResourceModel.OrganizationFee.ToString(), agent.UserDetail.FirstName, TaxRates.CashOutFeeRate.ToDescriptionString());

                        var result = _SMSService.sendMessage(message, agent.UserDetail.PhoneNumber);
                    }

                }

                return Ok(updatedRates);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [SystemLock]
        [HttpPost("SetAgentCommissionTaxRates")]
        public async Task<IActionResult> SetAgentCommissionTaxRates([FromBody] TaxRateResourceModel taxRateResourceModel)
        {
            try
            {
                //var updatedRates = await _transferFeeService.UpdateTransferFee(taxRates);
                if (taxRateResourceModel.AgentCommission.Equals(0)) return BadRequest(Messages.InvalidRequest.ToDescriptionString());

                var taxRates = await _transferFeeService.GetTransferFeeByDesc();
                taxRateResourceModel.AgentCommission = taxRateResourceModel.AgentCommission;
                taxRateResourceModel.OrganizationFee = taxRates.OrganizationFee;
                taxRateResourceModel.TaxAmount = taxRates.TaxAmount;
                taxRateResourceModel.Description = taxRates.Description;

                var updatedTaxRatesResource = _mapper.Map<TaxRateResourceModel, TransferFee>(taxRateResourceModel);
                var updatedRates = await _transferFeeService.AddTransferFee(updatedTaxRatesResource);
                if (updatedRates != null)
                {
                    var types = await _transferTypeService.GetTransferTypeById((int)TransferTypes.CashOut);
                    types.TransferFee = updatedRates.Id;
                    await _transferTypeService.UpdateTransferTypeById(types);

                    var allAgents = await _userDetailsService.GetAllUsersByRole((int)Role.Agent);
                    var agentsListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(allAgents);

                    foreach (var agent in agentsListResource)
                    {
                        var message = MessagesTemplates.TaxRateUpdateMessage(taxRateResourceModel.AgentCommission.ToString(), agent.UserDetail.FirstName, TaxRates.AgentCommisionRate.ToDescriptionString());

                        var result = _SMSService.sendMessage(message, agent.UserDetail.PhoneNumber);
                    }
                }
                return Ok(updatedRates);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }


        [HttpGet("TaxRates")]
        public async Task<IActionResult> GetAllTaxRatesHistory([FromQuery] PagingRequestForRates pagingRequestForRates)
        {
            try
            {
                var transferFees = await _transferFeeService.GetAllTransferFees(pagingRequestForRates.Index, pagingRequestForRates.Size);
                return Ok(transferFees);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestTaxRatesHistory([FromQuery] PagingRequestForRates pagingRequestForRates)
        {
            try
            {
                var transferFees = await _transferFeeService.GetTransferFeeByDesc();
                return Ok(transferFees);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


    }
}
