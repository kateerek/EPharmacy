using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EPharmacy.Data;
using EPharmacy.Data.Entities.Discounts;
using EPharmacy.ServerApp.Services.Discount;
using EPharmacy.ServerApp.Exceptions;
using FluentValidation.Results;
using EPharmacy.ServerApp.Models.Discounts.Models;
using EPharmacy.ServerApp.Models.Discounts.Responses;
using EPharmacy.ServerApp.Models.Discounts.Requests;

namespace EPharmacy.ServerApp.Controllers
{
    public class DiscountsController : SwaggerControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet("ActiveOffers")]
        public async Task<ActionResult<IEnumerable<DiscountInfoModel>>> GetActiveOffers()
        {
            return await _discountService.GetActiveOffers();
        }

        [HttpGet("PrescriptionCategories")]
        public async Task<ActionResult<IEnumerable<PrescriptionCategoryInfoModel>>> GetPrescriptionCategories()
        {
            return await _discountService.GetPrescriptionCategories();
        }

//      [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [HttpGet("Offer/{Id}")]
        public async Task<ActionResult<DiscountDetailsResponse>> GetOfferDetails([FromRoute] int Id)
        {
            return await _discountService.GetOfferDetails(Id);
        }

//      [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [HttpPost("Offer")]
        public async Task<ActionResult<DiscountDetailsResponse>> CreateOffer([FromBody] CreateOfferRequest createOfferRequest)
        {
            return await _discountService.CreateOffer(createOfferRequest);
        }

//      [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [HttpPut("Offer")]
        public async Task<ActionResult<DiscountDetailsResponse>> UpdateOffer([FromBody] UpdateOfferRequest updateOfferRequest)
        {
            return await _discountService.UpdateOffer(updateOfferRequest);
        }

//      [Authorize(Roles = DefaultRoles.Worker + "," + DefaultRoles.Admin)]
        [HttpDelete("Offer/{Id}")]
        public async Task DeleteOffer([FromRoute] int Id)
        {
            await _discountService.DeleteOffer(Id);
        }
    }
}