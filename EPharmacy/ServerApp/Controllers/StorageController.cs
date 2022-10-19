using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.ServerApp.Models.Common;
using EPharmacy.ServerApp.Models.Common.Responses;
using EPharmacy.ServerApp.Models.Storage.UploadFile;
using EPharmacy.ServerApp.Services.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using NSwag;
using NSwag.Annotations;

namespace EPharmacy.ServerApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StorageController : ControllerBase
    {

        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly IMapper _mapper;

        public StorageController(IAzureBlobStorageService azureBlobStorageService, IMapper mapper)
        {
            _azureBlobStorageService = azureBlobStorageService;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(UploadFileResponse), Description = "Returns URL to uploaded image")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if(file==null || file.Length == 0)
                return new BadRequestObjectResult(new StatusCode
                {
                    Status = "Error",
                    Message = "File not chosen"
                });
            try
            {
                var imgUrl = await _azureBlobStorageService.UploadFile(file.FileName, file.OpenReadStream());
                return Ok(_mapper.Map<UploadFileResponse>(imgUrl));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = e.Message
                });
            }
            
        }
                                        
    }
}