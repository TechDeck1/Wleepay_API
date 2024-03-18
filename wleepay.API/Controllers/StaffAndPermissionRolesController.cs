using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wleepay.Core.Services;

namespace wleepay.API.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    //[AllowAnonymous]


    public class StaffAndPermissionRolesController : Controller
    {

        private readonly ILookupService _lookupService;

        public StaffAndPermissionRolesController(
            ILookupService lookupService)
        {

            _lookupService = lookupService;
        }

        //[HttpGet("StaffRoles")]
        //public async Task<IActionResult> GetStaffRoles()
        //{
        //    var staffRoles = await _lookupService.GetAllStaffRoles();
        //    return Ok(staffRoles);
        //}

        //[HttpGet("StaffPermissions")]
        //public async Task<IActionResult> GetStaffPermissions()
        //{
        //    var staffRoles = await _lookupService.GetAllStaffPermissions();
        //    return Ok(staffRoles);
        //}
    }
    
}
