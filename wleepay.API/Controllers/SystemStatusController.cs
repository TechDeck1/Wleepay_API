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
            var result = await _systemStatusService.AddSystemStatus(systemStatus);
            return Ok(result);
        }
    }
}
