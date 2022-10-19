using EPharmacy.ServerApp.Models.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDefaultResponse]
    [SwaggerResponse(HttpStatusCode.BadRequest, typeof(BadRequestResponse))]
    [SwaggerResponse(HttpStatusCode.NotFound, typeof(NotFoundResponse))]
    [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(InternalServerErrorResponse))]
    public abstract class SwaggerControllerBase : ControllerBase
    {

    }
}
