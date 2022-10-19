using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EPharmacy.Data.Constants;
using EPharmacy.ServerApp.Models.Common.Responses;
using EPharmacy.ServerApp.Models.Product.ActiveSubstances;
using EPharmacy.ServerApp.Models.Product.Attributes;
using EPharmacy.ServerApp.Models.Product.Create;
using EPharmacy.ServerApp.Models.Product.Details;
using EPharmacy.ServerApp.Models.Product.Edit;
using EPharmacy.ServerApp.Models.Product.ProductsDetailsList;
using EPharmacy.ServerApp.Models.Product.ProductType.Create;
using EPharmacy.ServerApp.Models.Product.ProductType.GetAll;
using EPharmacy.ServerApp.Models.Product.Tags;
using EPharmacy.ServerApp.Services.Account;
using EPharmacy.ServerApp.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EPharmacy.ServerApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]    
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAccountService _accountService;

        public ProductController(IProductService productService, IAccountService accountService)
        {
            this._productService = productService;
            _accountService = accountService;
        }

        [HttpPost]
//        [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Create([FromBody] ProductCreationRequest productCreationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.CreateProduct(productCreationRequest);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Product has been created"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while creating new product"
                }) as IActionResult;
        }

        [HttpPut("{id}")]
//        [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Edit(int id, ProductEditionRequest productEditionRequest)
        {
            if (!ModelState.IsValid || productEditionRequest.Id != id)
                return BadRequest(ModelState);

            var result = await _productService.EditProduct(productEditionRequest);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Product has been edited properly"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while editing product"
                }) as IActionResult;
        }

        [HttpDelete("{id}")]
//        [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Remove(int  id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.RemoveProduct(id);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Attribute has been removed properly"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while removing attribute"
                }) as IActionResult;
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<ProductDetailsListModel>))]
        public async Task<IActionResult> ProductsByAttributes([FromQuery]ProductDetailsListRequest getRequest)
        {
            var products = await _productService.GetAllProductsByAttributes(getRequest);
            if (!User.Identity.IsAuthenticated) return new OkObjectResult(products);

            var user = await _accountService.FindUserByName(User.Identity.Name);
            products = await _productService.FillFavouriteStatusForProducts(products, user.Id);

            return new OkObjectResult(products);
        }
        
        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ProductDetailsModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Details(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.GetProductDetailsModel(id);

            if (User.Identity.IsAuthenticated)
            {
                var user = await _accountService.FindUserByName(User.Identity.Name);
                result = await _productService.FillFavouriteStatusForSingleProduct(result, user.Id);
            }
            return result != null
                ? new OkObjectResult(result)
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while getting product details"
                }) as IActionResult;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<AttributeModel>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Attributes(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.GetProductAttributesModels(id);
            return result != null
                ? new OkObjectResult(result)
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while getting product attributes"
                }) as IActionResult;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<AttributeTagModel>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> Tags(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.GetProductTags(id);
            return result != null
                ? new OkObjectResult(result)
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while getting product tags"
                }) as IActionResult;
        }

        [HttpPost]
//        [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> CreateProductType([FromBody] ProductTypeCreationRequest productTypeCreationRequestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.CreateProductType(productTypeCreationRequestModel);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Product type has been created properly"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while creating product type"
                }) as IActionResult;
        }

        [HttpGet]
        //        [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<ProductTypeModel>))]
        public async Task<IActionResult> GetAllProductTypes()
        {
            var result = await _productService.GetAllProductTypes();

            return new OkObjectResult(result);
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<ProductDetailsListModel>))]
        public async Task<IActionResult> GetDetailsForProducts([FromQuery]DetailsForProductsListRequest request)
        {
            var products = await _productService.GetDetailsForProducts(request.ProductIds);
            if (!User.Identity.IsAuthenticated) return new OkObjectResult(products);

            var user = await _accountService.FindUserByName(User.Identity.Name);
            products = await _productService.FillFavouriteStatusForProducts(products, user.Id);
            return new OkObjectResult(products);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StatusCode), Description = "Allowed only for user role")]
        [Authorize(Roles = DefaultRoles.User)]
        public async Task<IActionResult> ChangeIsFavouriteStatus(int id)
        {
            var user = await _accountService.FindUserByName(User.Identity.Name);
            var result = await _productService.ChangeIsFavouriteStatusForSingleProduct(id, user.Id);
            return result
                ? new OkObjectResult(new StatusCode
                {
                    Status = "OK",
                    Message = "Product favourite status has been changed"
                })
                : new BadRequestObjectResult(new StatusCode
                {
                    Status = "ERROR",
                    Message = "Error while changing favourite status"
                }) as IActionResult;
        }

        [HttpGet]
        //                [Authorize(Roles = DefaultRoles.User)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<ProductActiveSubstanceResponse>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> GetProductActiveSubstances(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.GetProductActiveSubstances(id);

            return new OkObjectResult(result);
            //return result != null
            //    ? new OkObjectResult(result)
            //    : new BadRequestObjectResult(new StatusCode
            //    {
            //        Status = "ERROR",
            //        Message = "Error while getting product active substances."
            //    }) as IActionResult;
        }

        [HttpGet]
        //                [Authorize(Roles = DefaultRoles.User)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<ProductDetailsListModel>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(StatusCode))]
        public async Task<IActionResult> GetProductSubstitutes(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.GetProductSubstitutes(id);
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
