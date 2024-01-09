using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.API.Controllers
{
    public class IssueTypeController : Controller
    {
        private readonly IIssueTypeService _issueTypeService;

        public IssueTypeController(
            IIssueTypeService issueTypeService)
        {

            _issueTypeService = issueTypeService;
        }

        [HttpGet("IssueTypes")]
        public async Task<IActionResult> GetAllIssueTypesAsync()
        {
            try
            {
                var issueTypes = _issueTypeService.GetAllIssueTypes();
                return Ok(issueTypes);

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
