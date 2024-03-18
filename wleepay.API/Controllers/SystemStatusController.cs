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

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    //[AllowAnonymous]

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
            if (systemStatusResource == null) {  return BadRequest(); }
            var systemStatus = _mapper.Map<SystemStatusResource, SystemStatus>(systemStatusResource);
            systemStatus.Status = systemStatusResource.Status;
            systemStatus.CreatedDate = DateTime.Now;
            var result = await _systemStatusService.AddUpdateSystemStatus(systemStatus);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetSystemStatus()
        {
            var result = await _systemStatusService.GetSystemStatus();
            statusHub.Clients.All.SendStatusToUser(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSystemStatusById(int id)
        {
            var result = await _systemStatusService.GetSystemStatusById(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSystemStatus([FromBody] SystemStatusResource systemStatusResource)
        {
            if(systemStatusResource.Id <= 0)
            {
                return BadRequest(Messages.NotUpdated.ToDescriptionString());

            }
            var systemStatus = _mapper.Map<SystemStatusResource, SystemStatus>(systemStatusResource);
            var result = await _systemStatusService.AddUpdateSystemStatus(systemStatus);
            return Ok(result);
        }
    }
}
