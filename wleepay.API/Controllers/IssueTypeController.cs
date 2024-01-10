using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.API.Controllers
{
    public class IssueTypeController : Controller
    {
        private readonly ILookupService _lookupService;

        public IssueTypeController(
            ILookupService lookupService)
        {

            _lookupService = lookupService;
        }

        [HttpGet("IssueTypes")]
        public async Task<IActionResult> GetAllIssueTypesAsync()
        {
            try
            {
                var issueTypes = await _lookupService.GetAllIssueTypes();
                return Ok(issueTypes);

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
