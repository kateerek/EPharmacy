using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EPharmacy.ServerApp.Models.Attributes.Create;
using EPharmacy.ServerApp.Models.Attributes.Edit;
using EPharmacy.ServerApp.Models.Attributes.GetAll;
using EPharmacy.ServerApp.Models.Common;
using EPharmacy.ServerApp.Models.Common.Responses;
using EPharmacy.ServerApp.Services.Attributes;
using EPharmacy.ServerApp.Models.Attributes.GetDetailsForAttributes;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EPharmacy.ServerApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttributeController : Controller
    {
        private readonly IAttributeService _attributeService;

        public AttributeController(IAttributeService attributeService)
        {
            this._attributeService = attributeService;
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Create([FromBody] AttributeCreationRequest attributeCreationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _attributeService.CreateAttribute(attributeCreationRequest);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Attribute has been created"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while creating new attribute"
                }) as IActionResult;
        }

        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Edit(int id, AttributeEditionRequest attributeEditionRequest)
        {
            if (!ModelState.IsValid || attributeEditionRequest.AttributeToEditId != id)
                return BadRequest(ModelState);

            var result = await _attributeService.EditAttribute(attributeEditionRequest);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Attribute has been edited properly"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while editing attribute"
                }) as IActionResult;
            
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<AttributeResponseModel>))]
        public async Task<IActionResult> GetAll()
        {
            var attributes = await _attributeService.GetAllAttributes();
            return new OkObjectResult(attributes.OrderBy(x => x.Name));
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<AttributeResponseModel>))]
        public async Task<IActionResult> GetAttributesDetails([FromQuery]AttributesDetailsListRequest request)
        {
            var attributes = await _attributeService.GetDetailsForAttributes(request.AttributeIds);
            return new OkObjectResult(attributes.OrderBy(x => x.Name));
        }

        [HttpGet()]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<CategoryModel>))]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _attributeService.GetCategories();
            return new OkObjectResult(categories);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _attributeService.RemoveAttribute(id);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Attribute has been deleted"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while deleting attribute"
                }) as IActionResult;
        }

    }
}
