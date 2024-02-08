using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Core.Services;

using wleepay.Core.Models;
using AutoMapper;
using System;

namespace wleepay.API.Controllers
{
    public class SystemStatusController : Controller
    {
        private readonly ISystemStatusService _systemStatusService;
        private readonly IMapper _mapper;


        public SystemStatusController(
            ISystemStatusService statusService, IMapper mapper)
        {

            _systemStatusService = statusService;
            _mapper = mapper;
        }

        [HttpPost("AddSystemsystem")]
        public async Task<IActionResult> AddSystemStatus([FromBody]SystemStatusResource systemStatusResource)
        {
            if (systemStatusResource == null) {  return BadRequest(); }
            var systemStatus = _mapper.Map<SystemStatusResource, SystemStatus>(systemStatusResource);
            systemStatus.Status = systemStatusResource.Status;
            systemStatus.CreatedDate = DateTime.Now;
            var result = await _systemStatusService.AddUpdateSystemStatus(systemStatus);
            return Ok(result);
        }

        [HttpGet("GetSystemStatus")]
        public async Task<IActionResult> GetSystemStatus()
        {
            var result = await _systemStatusService.GetSystemStatus();
            return Ok(result);
        }

        [HttpGet("GetSystemStatusById/{id}")]
        public async Task<IActionResult> GetSystemStatusById(int id)
        {
            var result = await _systemStatusService.GetSystemStatusById(id);
            return Ok(result);
        }

        [HttpPut("UpdateSystemStatus")]
        public async Task<IActionResult> UpdateSystemStatus([FromBody] SystemStatusResource systemStatusResource)
        {
            if(systemStatusResource.Id <= 0)
            {
                return BadRequest("Cannont update status id is null");

            }
            var systemStatus = _mapper.Map<SystemStatusResource, SystemStatus>(systemStatusResource);
            var result = await _systemStatusService.AddUpdateSystemStatus(systemStatus);
            return Ok(result);
        }
    }
}
