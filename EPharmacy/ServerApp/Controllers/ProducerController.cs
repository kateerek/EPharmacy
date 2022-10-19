using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EPharmacy.ServerApp.Models.Producer.Common;
using EPharmacy.ServerApp.Models.Common.Responses;
using EPharmacy.ServerApp.Models.Producer.ProducerCreation;
using EPharmacy.ServerApp.Services.Producer;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EPharmacy.ServerApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProducerController : Controller
    {
        private readonly IProducerService _producerService;

        public ProducerController(IProducerService  producerService)
        {
            this._producerService = producerService;
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Create([FromBody] ProducerCreationRequestModel producerCreationRequestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _producerService.CreateProducer(producerCreationRequestModel);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Producer has been created"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while creating new producer"
                }) as IActionResult;
        }

        [HttpGet]        
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<ProducerModel>))]
        public async Task<IActionResult> GetAll()
        {
            var producers = await _producerService.GetAllProducers();
            return new OkObjectResult(producers);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Edit(int id, ProducerModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
                return BadRequest(ModelState);
            var result = await _producerService.EditProducer(model);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Producer has been edited properly"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while editing producer"
                }) as IActionResult;
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _producerService.RemoveProducer(id);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Producer has been deleted"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while deleting producer"
                }) as IActionResult;
        }
    }
}
