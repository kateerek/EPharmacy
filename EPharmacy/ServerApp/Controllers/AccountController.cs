using System.Net;
using System.Threading.Tasks;
using EPharmacy.Data.Constants;
using EPharmacy.ServerApp.Models.Account.Requests;
using EPharmacy.ServerApp.Models.Account.Responses;
using EPharmacy.ServerApp.Models.Common.Responses;
using EPharmacy.ServerApp.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EPharmacy.ServerApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Create([FromBody] RegistrationRequest registrationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.CreateUserWithRole(registrationRequest, DefaultRoles.User);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "User has been created"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while creating new user"
                }) as IActionResult;
        }
        
        [HttpPost]
//        [Authorize(Roles = DefaultRoles.Admin)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> CreateWorker([FromBody] RegistrationRequest registrationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.CreateUserWithRole(registrationRequest, DefaultRoles.Worker);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "User has been created"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while creating new user"
                }) as IActionResult;
        }


        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(LoginResponse), Description = "Token")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginResponse = await _accountService.GetClaimsIdentity(loginRequest);

            return loginResponse != null
                ? new OkObjectResult(loginResponse)
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "User not found"
                }) as IActionResult;
        }

        [HttpGet]
        [Authorize]        
        [SwaggerResponse(HttpStatusCode.OK, typeof(UserData),Description = "Returns user data for given JWT token")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> GetData()
        {
            var userName = HttpContext.User.Identity.Name;
            var userData = await _accountService.GetUserData(userName);
            return userData != null
                ? new OkObjectResult(userData)
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "User not found"
                }) as IActionResult;
        }

        [Authorize]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(LoginResponse), Description = "Token")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> RenewToken()
        {
            var encodedToken = ExtractJwtTokenFromHeader();
            var response = await _accountService.GetClaimsIdentity(encodedToken);
            return response != null
                ? new OkObjectResult(response)
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Internal error"
                }) as IActionResult;
        }

        [Authorize]
        [HttpPost]
        [SwaggerDefaultResponse]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> ChangeData([FromBody] UserData userData)
        {
            var userName = HttpContext.User.Identity.Name;
            return await _accountService.ChangeUserData(userName, userData)
                ? Ok()
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "User not found"
                }) as IActionResult;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeRequest passwordChangeRequest)
        {
            var userName = User.Identity.Name;
            return await _accountService.ChangeUserPassword(userName, passwordChangeRequest.CurrentPassword, passwordChangeRequest.NewPassword)
                ? Ok()
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Operation failed."
                }) as IActionResult;

        }

        private string ExtractJwtTokenFromHeader()
        {
            return Request.Headers["Authorization"].ToString().Remove(0, 7); // get rid of 'Bearer' prefix
        }
    }
}