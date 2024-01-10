using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Storage.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SecurityQuestionController : Controller
    {

        private readonly ILookupService _lookupService;

        public SecurityQuestionController(
            ILookupService lookupService )
        {

            _lookupService = lookupService;
        }

        [HttpGet("SecurityQuestions")]
        public async Task<IActionResult> GetSecurityQuestions()
        {
            var questions = await _lookupService.GetAllSecurityQuestions();
            return Ok(questions);
        }
    }
}
