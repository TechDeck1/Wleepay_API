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
    [AllowAnonymous]
    public class SecurityQuestionController : Controller
    {
        private readonly IUserDetailService _userDetailsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly ISecurityQuestionService _securityQuestionService;

        public SecurityQuestionController(UserManager<ApplicationUser> userManager,
            IUserDetailService userDetailsService,
            IMapper mapper,
            ILogger<UserController> logger,
            ISecurityQuestionService securityQuestionService)
        {
            _userManager = userManager;
            _userDetailsService = userDetailsService;
            _mapper = mapper;
            _logger = logger;
            _securityQuestionService = securityQuestionService;
        }

        [HttpGet("SecurityQuestions")]
        public async Task<IActionResult> GetSecurityQuestions()
        {
            var questions = _securityQuestionService.GetAllSecurityQuestions();
            //var questionResource = _mapper.Map<SecurityQuestions, SecurityQuestionResource>((SecurityQuestions)questions);
            return Ok(questions);
        }
    }
}
