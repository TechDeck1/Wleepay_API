using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using wleepay.Storage.Interface;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class DocumentController : Controller
    {
        private readonly IUploadDocument _documentUploaderService;
        public DocumentController(IUploadDocument uploadDocument)
        {
            _documentUploaderService = uploadDocument;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> uploadDocument(IFormFile file, string userId, string containerName = "kyc-document")
        {
            try
            {
                var getFileExtension = Path.GetExtension(file.FileName);
                var generatedNewFileName = $"{Guid.NewGuid().ToString()}{getFileExtension}";
                var ms = new MemoryStream();

                await file.CopyToAsync(ms);
                ms.ToArray();
                ms.Seek(0, SeekOrigin.Begin);
                var filePath = await _documentUploaderService.UploadData(ms, generatedNewFileName, containerName, getFileExtension, userId);
                return Ok(filePath);
            }
            catch (Exception Ex)
            {
                throw Ex.InnerException;
            }
        }
    }
}
