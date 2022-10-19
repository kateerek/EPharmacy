using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPharmacy.Data.Entities.SalesOrders;
using EPharmacy.ServerApp.Models.Attributes.GetAll;
using EPharmacy.ServerApp.Models.BusinessIntelligence.Requests;
using EPharmacy.ServerApp.Models.BusinessIntelligence.Responses;
using EPharmacy.ServerApp.Models.Discounts.Models;
using EPharmacy.ServerApp.Models.Product.Common;
using Microsoft.AspNetCore.Mvc;

namespace EPharmacy.ServerApp.Services.BusinessIntelligence
{
    public interface IBusinessIntelligenceService
    {
        Task<BestSellingResponse<ProductShortModel>> GetBestSellingProducts(BestSellingRequest bestSellingRequest);
        Task<BestSellingResponse<AttributeResponseModel>> GetBestSellingAttributes(BestSellingRequest bestSellingRequest);
        Task<BestSellingResponse<DiscountInfoModel>> GetBestSellingOfferDiscounts(BestSellingRequest bestSellingRequest);
        Task<BestSellingResponse<PrescriptionCategoryInfoModel>> GetBestSellingPrescriptionDiscounts(BestSellingRequest bestSellingRequest);
        Task<BestSellingResponse<PharmacyLocation>> GetBestSellingByPharmacyLocation(BestSellingRequest bestSellingRequest);
    }
}
