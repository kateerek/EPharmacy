using EPharmacy.Data.Entities.SalesOrders;
using EPharmacy.ServerApp.Models.Attributes.GetAll;
using EPharmacy.ServerApp.Models.BusinessIntelligence.Requests;
using EPharmacy.ServerApp.Models.BusinessIntelligence.Responses;
using EPharmacy.ServerApp.Models.Discounts.Models;
using EPharmacy.ServerApp.Models.Product.Common;
using EPharmacy.ServerApp.Services.BusinessIntelligence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Controllers
{
    public class BusinessIntelligenceController : SwaggerControllerBase
    {
        private readonly IBusinessIntelligenceService _businessIntelligenceService;

        public BusinessIntelligenceController(IBusinessIntelligenceService businessIntelligenceService)
        {
            _businessIntelligenceService = businessIntelligenceService;
        }

//      [Authorize(Roles = DefaultRoles.Admin)]
        [HttpGet("BestSellingProducts")]
        public async Task<ActionResult<BestSellingResponse<ProductShortModel>>> GetBestSellingProducts([FromQuery] BestSellingRequest bestSellingRequest)
        {
            return await _businessIntelligenceService.GetBestSellingProducts(bestSellingRequest);
        }

//      [Authorize(Roles = DefaultRoles.Admin)]
        [HttpGet("BestSellingAttributes")]
        public async Task<ActionResult<BestSellingResponse<AttributeResponseModel>>> GetBestSellingAttributes([FromQuery] BestSellingRequest bestSellingRequest)
        {
            return await _businessIntelligenceService.GetBestSellingAttributes(bestSellingRequest);
        }

//      [Authorize(Roles = DefaultRoles.Admin)]
        [HttpGet("BestSellingOfferDiscounts")]
        public async Task<ActionResult<BestSellingResponse<DiscountInfoModel>>> GetBestSellingOfferDiscounts([FromQuery] BestSellingRequest bestSellingRequest)
        {
            return await _businessIntelligenceService.GetBestSellingOfferDiscounts(bestSellingRequest);
        }

//      [Authorize(Roles = DefaultRoles.Admin)]
        [HttpGet("BestSellingPrescriptionDiscounts")]
        public async Task<ActionResult<BestSellingResponse<PrescriptionCategoryInfoModel>>> GetBestSellingPrescriptionDiscounts([FromQuery] BestSellingRequest bestSellingRequest)
        {
            return await _businessIntelligenceService.GetBestSellingPrescriptionDiscounts(bestSellingRequest);
        }

        //      [Authorize(Roles = DefaultRoles.Admin)]
        [HttpGet("BestSellingByPharmacyLocation")]
        public async Task<ActionResult<BestSellingResponse<PharmacyLocation>>> GetBestSellingByPharmacyLocation([FromQuery] BestSellingRequest bestSellingRequest)
        {
            return await _businessIntelligenceService.GetBestSellingByPharmacyLocation(bestSellingRequest);
        }
    }
}
