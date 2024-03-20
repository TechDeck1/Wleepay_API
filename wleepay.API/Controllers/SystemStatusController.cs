using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Core.Services;
using wleepay.Core.Models;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using wleepay.API.Hub;
using wleepay.API.Middleware;
using wleepay.Core.Enums;
using wleepay.Core.Common;
using wleepay.API.Validators;
using FluentValidation;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class SystemStatusController : Controller
    {
        private readonly ISystemStatusService _systemStatusService;
        private readonly IMapper _mapper;
        private IHubContext<StatusHubClient, IStatusHubClient> statusHub;

        public SystemStatusController(
            ISystemStatusService statusService,
            IMapper mapper,
            IHubContext<StatusHubClient, IStatusHubClient> _statusHub)
        {
            _systemStatusService = statusService;
            _mapper = mapper;
            statusHub = _statusHub;

        }


        [HttpPost]
        public async Task<IActionResult> AddSystemStatus([FromBody]SystemStatusResource systemStatusResource)
        {
            try
            {
                var validator = new SystemStatusValidator();
                var validationResult = await validator.ValidateAsync(systemStatusResource);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                if (systemStatusResource == null) { return BadRequest(); }
                var systemStatus = _mapper.Map<SystemStatusResource, SystemStatus>(systemStatusResource);
                systemStatus.Status = systemStatusResource.Status;
                systemStatus.CreatedDate = DateTime.Now;
                var result = await _systemStatusService.AddUpdateSystemStatus(systemStatus);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
           
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetSystemStatus()
        {
            try
            {
                var result = await _systemStatusService.GetSystemStatus();
                statusHub.Clients.All.SendStatusToUser(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSystemStatusById(int id)
        {
            try
            {
                var result = await _systemStatusService.GetSystemStatusById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
           
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSystemStatus([FromBody] SystemStatusResource systemStatusResource)
        {
            try
            {
                if (systemStatusResource.Id <= 0)
                {
                    return BadRequest(Messages.NotUpdated.ToDescriptionString());
                }
                var systemStatus = _mapper.Map<SystemStatusResource, SystemStatus>(systemStatusResource);
                var result = await _systemStatusService.AddUpdateSystemStatus(systemStatus);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
         
        }
    }
}
