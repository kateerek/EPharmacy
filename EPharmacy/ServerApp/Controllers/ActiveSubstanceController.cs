using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EPharmacy.ServerApp.Models.ActiveSubstance.Create;
using EPharmacy.ServerApp.Models.ActiveSubstance.GetAll;
using EPharmacy.ServerApp.Models.Common.Responses;
using EPharmacy.ServerApp.Services.ActiveSubstance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSwag.Annotations;

namespace EPharmacy.ServerApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ActiveSubstanceController : Controller
    {
        private readonly IActiveSubstanceService _activeSubstanceService;

        public ActiveSubstanceController(IActiveSubstanceService activeSubstanceService)
        {
            this._activeSubstanceService = activeSubstanceService;
        }

        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Create(
            [FromBody] ActiveSubstanceCreationRequest activeSubstanceCreationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _activeSubstanceService.CreateActiveSubstance(activeSubstanceCreationRequest);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Active substance has been created"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while creating new active substance"
                }) as IActionResult;
        }

        [HttpDelete("{id}")]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _activeSubstanceService.RemoveActiveSubstance(id);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Active substance has been removed"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while removing active substance"
                }) as IActionResult;
        }

        [HttpGet]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<ActiveSubstanceResponse>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _activeSubstanceService.GetAllActiveSubstances();
            return new OkObjectResult(result);
            //return result != null
            //    ? new OkObjectResult(result)
            //    : new BadRequestObjectResult(new StatusCode
            //    {
            //        Status = "ERROR",
            //        Message = "Error while getting product substiutes."
            //    }) as IActionResult;
        }
    }
}
