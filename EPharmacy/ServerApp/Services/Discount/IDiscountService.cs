using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPharmacy.Data.Entities.Products;
using EPharmacy.ServerApp.Models.Discounts.Models;
using EPharmacy.ServerApp.Models.Discounts.Requests;
using EPharmacy.ServerApp.Models.Discounts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EPharmacy.ServerApp.Services.Discount
{
    public interface IDiscountService
    {
        Task<ProductDiscountResponse> GetDiscountsForProduct(Product product);
        Task<List<DiscountInfoModel>> GetActiveOffers();
        Task<List<PrescriptionCategoryInfoModel>> GetPrescriptionCategories();
        Task<DiscountDetailsResponse> GetOfferDetails(int id);
        Task<DiscountDetailsResponse> CreateOffer(CreateOfferRequest request);
        Task<DiscountDetailsResponse> UpdateOffer(UpdateOfferRequest request);
        Task DeleteOffer(int id);
        Task ModifyPrescriptionDiscountsForProduct(Product product, List<PrescriptionDiscountModel> prescriptionDiscounts);
        Task RemovePrescriptionDiscountsForProduct(Product product);

        Task<(decimal? PriceWithDiscount, int? DiscountCategoryId)> CalculatePriceWithDiscount(int productId, int discountId);
    }
}
