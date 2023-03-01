using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.API.Resources;
using wleepay.Services;
using wleepay.API.Validators;
using Microsoft.AspNetCore.Authorization;
using wleepay.Core.Services;
using wleepay.Communication.Services;
using wleepay.Communication.Interface;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KYCController : Controller
    {
        private readonly IKYCDocumentsService _kycDocumentsService;
        private readonly ILookupService _lookupService;
        private readonly ISmileIdentityService _smileIdentityService;
        private readonly IKYCLevelService _kycLevelService;
        private readonly IMapper _mapper;
        public KYCController(ILookupService lookupService, IKYCDocumentsService kycDocumentsService, ISmileIdentityService smileIdentityService, IKYCLevelService kycLevelService, IMapper mapper)
        {
            _lookupService = lookupService;
            _kycDocumentsService = kycDocumentsService;
            _smileIdentityService = smileIdentityService;
            _kycLevelService = kycLevelService;
            _mapper = mapper;
        }

        [HttpGet("DocumentTypes")]
        public async Task<IActionResult> GetAllKYCDocumentTypes()
        {
            var documentTypes = await _lookupService.GetAllDocumentType();
            var documentTypeResource = _mapper.Map<IEnumerable<DocumentType>, IEnumerable<LookupResource>>(documentTypes);
            return Ok(documentTypeResource);
        }

        [HttpGet("Status")]
        public async Task<IActionResult> GetAllKYCStatus()
        {
            var kycStatusTypes = await _lookupService.GetAllKYCStatus();
            var kycStatusTypeResource = _mapper.Map<IEnumerable<wleepay.Core.Models.KYCStatus>, IEnumerable<LookupResource>>(kycStatusTypes);
            return Ok(kycStatusTypeResource);
        }

        [HttpPost("documents")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddKYCDocuments([FromBody] IEnumerable<KYCDocumentResource> addKYCDocumentResource)
        {
            var validator = new AddKYCDocumentValidator();
            var validationResult = await validator.ValidateAsync(addKYCDocumentResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var kycDocumentToAdd = _mapper.Map<IEnumerable<KYCDocumentResource>, IEnumerable<KYCdocuments>>(addKYCDocumentResource);

            List<Task<KYCdocuments>> taskList = new List<Task<KYCdocuments>>();

            foreach (var document in kycDocumentToAdd)
            {
                taskList.Add(_kycDocumentsService.AddOrUpdateKYCDocument(document));
            }

            var addedDocuments = await Task.WhenAll<KYCdocuments>(taskList);

            var addedDocumentResource = _mapper.Map<IEnumerable<KYCdocuments>, IEnumerable<KYCDocumentResource>>(addedDocuments);

            return Ok(addedDocumentResource);
        }

        [HttpGet("level")]
        public async Task<IActionResult> GetKYCLevels()
        {
            var kycLevels = await _kycLevelService.GetAll();
            var kycLevelsResource = _mapper.Map<IEnumerable<Core.Models.KYCLevel>, IEnumerable<Resources.KYCLevel>>(kycLevels);

            return Ok(kycLevelsResource);
        }

        [HttpGet("level/{id}")]
        public async Task<IActionResult> GetKYCLevels(int id)
        {
            var kycLevel = await _kycLevelService.GetById(id);
            var kycLevelResource = _mapper.Map<Core.Models.KYCLevel, Resources.KYCLevel>(kycLevel);

            return Ok(kycLevelResource);
        }

        [HttpDelete("reEnrollSmileIdentityUser/{userId}")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ReEnrollSmileIdentityUser(string userId)
        {
            var result = await _smileIdentityService.ReEnrollUser(userId);
            return Ok(result);
        }
    }
}
