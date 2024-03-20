using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Data.Interface;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class CountryController : Controller
    {
        private readonly ICountyService _countyService;
        private readonly IMapper _mapper;

        public CountryController(ICountyService countyService, IMapper mapper)
        {
            _countyService = countyService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<LookupResource>> GetAllCounties()
        {
            try
            {
                var countyList = await _countyService.GetAll();
                var countyListResource = _mapper.Map<IEnumerable<wleepay.Core.Models.County>, IEnumerable<LookupResource>>(countyList).ToList();
                countyListResource = countyListResource.OrderBy(x => x.Name).ToList();
                return countyListResource;
            }catch(Exception ex)
            {
                throw;
            }
           
            
        }
    }
}
