using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EPharmacy.Data.Constants;
using EPharmacy.ServerApp.Models.Common.Responses;
using EPharmacy.ServerApp.Models.SalesOrder.Create;
using EPharmacy.ServerApp.Models.SalesOrder.GetForUser;
using EPharmacy.ServerApp.Services.Account;
using EPharmacy.ServerApp.Services.Discount;
using EPharmacy.ServerApp.Services.SalesOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EPharmacy.ServerApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalesOrderController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IDiscountService _discountService;
        private readonly ISalesOrderService _salesOrderService;


        public SalesOrderController(IAccountService accountService, ISalesOrderService salesOrderService,
            IDiscountService discountService)
        {
            _accountService = accountService;
            _salesOrderService = salesOrderService;
            _discountService = discountService;
        }

        [HttpGet]
        [Authorize(Roles = DefaultRoles.User)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<SalesOrderResponse>))]
        public async Task<IActionResult> GetForUser()
        {
            var user = await _accountService.FindUserByName(User.Identity.Name);
            var list = await _salesOrderService.GetSalesOrdersForUserId(user.Id);
            return new OkObjectResult(list);
        }

        [HttpPost]
        [Authorize(Roles = DefaultRoles.User)]
        [SwaggerDefaultResponse]
        public async Task<IActionResult> Create(SalesOrderRequest salesOrderRequest)
        {
            var user = await _accountService.FindUserByName(User.Identity.Name);
            var entity = _salesOrderService.Map(salesOrderRequest);
            foreach (var item in entity.Items)
            {
                var (priceWithDiscount, discountCategoryId) =
                    await _discountService.CalculatePriceWithDiscount(item.ProductId, item.DiscountId ?? 0);
                item.PriceWithDiscount = priceWithDiscount;
                item.DiscountCategoryId = discountCategoryId;
            }

            var result = await _salesOrderService.CreateSalesOrder(entity, user.Id);
            return result ?
                Ok():
                BadRequest() as IActionResult;
        }

        [HttpPut("{id}")]
//        [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [SwaggerDefaultResponse]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> SetAsCompleted(int id)
        {
            var result = await _salesOrderService.SetAsCompleted(id);
            return result ?
                Ok():
                new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Sales order not found or is actually completed"
                }) as IActionResult;
        }
        
        [HttpGet]
//        [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<SalesOrderResponse>))]
        public async Task<IActionResult> GetInProgress()
        {
            var results = await _salesOrderService.GetInProgress();
            return new OkObjectResult(results);
        }
        
        [HttpGet]
//        [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<SalesOrderResponse>))]
        public async Task<IActionResult> GetCompleted()
        {
            var results = await _salesOrderService.GetCompleted();
            return new OkObjectResult(results);
        }
    }
}