using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EPharmacy.Data.Constants;
using EPharmacy.ServerApp.Models.Common;
using EPharmacy.ServerApp.Models.Common.Responses;
using EPharmacy.ServerApp.Models.Pharmacy.AddLocation;
using EPharmacy.ServerApp.Models.Pharmacy.Common;
using EPharmacy.ServerApp.Services.Pharmacy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EPharmacy.ServerApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PharmacyController : Controller
    {
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<PharmacyLocationModel>))]
        public async Task<IActionResult> GetLocations()
        {
            var locations = await _pharmacyService.GetAllPharmacies();
            return new OkObjectResult(locations);
        }

        [HttpPost]
        [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [SwaggerDefaultResponse]
        public async Task<IActionResult> AddLocation(PharmacyLocationRequest pharmacyLocationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _pharmacyService.AddPharmacyLocation(pharmacyLocationRequest);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> EditLocation(int id, PharmacyLocationModel pharmacyLocationModel)
        {
            if (!ModelState.IsValid || id != pharmacyLocationModel.Id)
                return BadRequest(ModelState);

            var result = await _pharmacyService.EditPharmacyLocation(pharmacyLocationModel);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Pharmacy location has been edited properly"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while editing pharmacy location"
                }) as IActionResult;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var result = await _pharmacyService.RemovePharmacyLocation(id);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Pharmacy location has been deleted"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while deleting pharmacy location"
                }) as IActionResult;
        }
    }
}