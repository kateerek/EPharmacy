using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data;
using EPharmacy.Data.Entities.Discounts;
using EPharmacy.Data.Entities.Products;
using EPharmacy.ServerApp.Models.Discounts.Models;
using EPharmacy.ServerApp.Models.Discounts.Responses;
using Microsoft.EntityFrameworkCore;
using EPharmacy.ServerApp.Common.Helpers;
using EPharmacy.ServerApp.Exceptions;
using EPharmacy.ServerApp.Models.Discounts.Requests;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EPharmacy.ServerApp.Services.Discount
{
    using DiscountEntity = Data.Entities.Discounts.Discount;
    public class DiscountService : IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly EPharmacyContext _context;

        public DiscountService(EPharmacyContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductDiscountResponse> GetDiscountsForProduct(Product product)
        {
            return new ProductDiscountResponse()
            {
                PrescriptionDiscounts = await GetPrescriptionDiscounts(product),
                OfferDiscount = await GetOfferDiscount(product)
            };
        }

        public Task<List<DiscountInfoModel>> GetActiveOffers()
        {
            return _context.Discounts.GetOffers()
                    .GetActiveDiscounts()
                    .Select(x => _mapper.Map<DiscountInfoModel>(x))
                    .ToListAsync();
        }

        public Task<List<PrescriptionCategoryInfoModel>> GetPrescriptionCategories()
        {
            return _context.DiscountCategories.Select(x => _mapper.Map<PrescriptionCategoryInfoModel>(x))
                    .ToListAsync();
        }

        public async Task<DiscountDetailsResponse> GetOfferDetails(int id)
        {
            var discount = await GetOfferByIdAsync(id);

            return new DiscountDetailsResponse
            {
                Discount = _mapper.Map<DiscountDetailsModel>(discount),
                Products = await _context.ProductDiscounts
                                                     .Where(x => x.DiscountId == id)
                                                     .Select(x => x.ProductId)
                                                     .ToListAsync(),
                Attributes = await _context.AttributeDiscounts
                                                    .Where(x => x.DiscountId == id)
                                                    .Select(x => x.AttributeId)
                                                    .ToListAsync()
            };
        }

        public Task<DiscountDetailsResponse> CreateOffer(CreateOfferRequest request)
        {
            return DbQueryHelper.RunWithTransactionAsync(async () => {
                var discount = _mapper.Map<DiscountEntity>(request);
                await _context.Discounts.AddAsync(discount);
                discount.ProductDiscounts = await CreateProductDiscounts(discount, request);
                discount.AttributeDiscounts = await CreateAttributeDiscounts(discount, request);
                if (!(await _context.SaveChangesAsync() > 0))
                    throw new InternalServerErrorException(
                        $"Wystąpił błąd podczas tworzenia nowej zniżki",
                        $"Create of Discount failed:\n{JsonConvert.SerializeObject(request)}");
                return CreateDiscountDetailsResponse(discount);
            }, _context);
        }

        public Task<DiscountDetailsResponse> UpdateOffer(UpdateOfferRequest request)
        {
            return DbQueryHelper.RunWithTransactionAsync(async () => {
                var id = request.Id;
                var discount = await GetOfferByIdAsync(id);
                _context.Discounts.Update(discount);
                _mapper.Map(request, discount);
                await UpdateProductDiscounts(discount, request);
                await UpdateAttributeDiscounts(discount, request);
                if (!(await _context.SaveChangesAsync() > 0))
                    throw new InternalServerErrorException(
                        $"Wystąpił błąd podczas modyfikowania zniżki o Id: {id}",
                        $"Update of Discount({id}) failed:\n{JsonConvert.SerializeObject(request)}");
                return CreateDiscountDetailsResponse(discount);
            }, _context);
        }

        public Task DeleteOffer(int id)
        {
            return DbQueryHelper.RunWithTransactionAsync(async () => {
                var offer = await GetOfferByIdAsync(id);
                _context.Discounts.Remove(offer);
                if (!(await _context.SaveChangesAsync() > 0))
                    throw new InternalServerErrorException(
                        $"Nie udało się usunąć zniżki o Id: {id}",
                        $"Deletion of Discount({id}) failed");
            }, _context);
        }

        public async Task ModifyPrescriptionDiscountsForProduct(Product product, List<PrescriptionDiscountModel> prescriptionDiscounts)
        {
            await ValidatePrescriptionDiscounts(prescriptionDiscounts);
            var productPrescriptionDiscounts = await _context.ProductDiscounts
                                                    .Where(x => x.Product == product)
                                                    .Select(x => x.Discount)
                                                    .GetPrescriptions()
                                                    .ToListAsync();
            foreach (var reqDiscount in prescriptionDiscounts)
            {
                var productDiscount = productPrescriptionDiscounts.Find(
                    x => x.DiscountCategoryId == reqDiscount.PrescriptionCategoryId);
                if (productDiscount != null)
                {
                    _mapper.Map(reqDiscount, productDiscount);
                    _context.Discounts.Update(productDiscount);
                }
                else
                {
                    var discount = _mapper.Map<DiscountEntity>(reqDiscount);
                    await _context.Discounts.AddAsync(discount);
                    var productDiscountToAdd = new ProductDiscount() { Discount = discount, Product = product };
                    await _context.ProductDiscounts.AddAsync(productDiscountToAdd);
                }
            }
            var discountsToRemove = productPrescriptionDiscounts.Select(x => x.DiscountCategoryId ?? 0)
                        .Except(prescriptionDiscounts.Select(x => x.PrescriptionCategoryId))
                        .Join(productPrescriptionDiscounts, x => x, x => x.DiscountCategoryId ?? 0, (x, y) => y);
            _context.Discounts.RemoveRange(discountsToRemove);
            if (!(await _context.SaveChangesAsync() > 0))
                throw new InternalServerErrorException(
                    $"Nie udało się zmodyfikować zniżek dla Produktu({product.Id}): {product.Name}",
                    $"ModifyPrescriptionDiscounts of Product({product.Id}) failed");
        }

        public async Task RemovePrescriptionDiscountsForProduct(Product product)
        {
            var discountsToRemove = await _context.ProductDiscounts
                                            .Where(x => x.Product == product)
                                            .Select(x => x.Discount)
                                            .GetPrescriptions()
                                            .ToListAsync();
            if(discountsToRemove.Count != 0)
            {
                _context.Discounts.RemoveRange(discountsToRemove);
                if (!(await _context.SaveChangesAsync() > 0))
                    throw new InternalServerErrorException(
                        $"Nie udało się usunąć zniżek dla Produktu({product.Id}): {product.Name}",
                        $"RemovePrescriptionDiscounts of Product({product.Id}) failed");
            }
        }

        public async Task<(decimal? PriceWithDiscount, int? DiscountCategoryId)> CalculatePriceWithDiscount(int productId, int discountId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            (decimal? PriceWithDiscount, int? DiscountCategoryId) result = (null, null);
            if (product == null)
            {
                return result;
            }
            result = await CalculatePrescriptionDiscount(product, discountId);
            if (result.DiscountCategoryId == null)
            {
                var discountModel = await GetOfferDiscount(product);
                if (discountModel != null)
                {
                    result.PriceWithDiscount = discountModel.Price;
                }
            }
            return result;
        }

        private async Task UpdateAttributeDiscounts(DiscountEntity discount, UpdateOfferRequest request)
        {
            var requestedProductDiscounts = await CreateProductDiscounts(discount, request);
            foreach (var productDiscountToRemove in discount.ProductDiscounts.Except(requestedProductDiscounts).ToArray())
            {
                discount.ProductDiscounts.Remove(productDiscountToRemove);
            }
            discount.ProductDiscounts.AddRange(requestedProductDiscounts.Except(discount.ProductDiscounts));
        }

        private async Task UpdateProductDiscounts(DiscountEntity discount, UpdateOfferRequest request)
        {
            var requestedAttributeDiscounts = await CreateAttributeDiscounts(discount, request);
            foreach (var attributeDiscountToRemove in discount.AttributeDiscounts.Except(requestedAttributeDiscounts).ToArray())
            {
                discount.AttributeDiscounts.Remove(attributeDiscountToRemove);
            }
            discount.AttributeDiscounts.AddRange(requestedAttributeDiscounts.Except(discount.AttributeDiscounts));
        }

        private async Task ValidatePrescriptionDiscounts(List<PrescriptionDiscountModel> prescriptionDiscounts)
        {
            var categories = await _context.DiscountCategories.ToListAsync();
            var diff = prescriptionDiscounts.Select(x => x.PrescriptionCategoryId)
                                            .Except(categories.Select(x => x.Id));
            if (diff.Any())
            {
                throw CreateValidationException(nameof(PrescriptionDiscountModel), diff);
            }
        }

        private DiscountDetailsResponse CreateDiscountDetailsResponse(DiscountEntity discount)
        {
            return new DiscountDetailsResponse()
            {
                Discount = _mapper.Map<DiscountDetailsModel>(discount),
                Products = discount.ProductDiscounts.Select(x => x.ProductId).ToArray(),
                Attributes = discount.AttributeDiscounts.Select(x => x.AttributeId).ToArray()
            };
        }

        private async Task<List<ProductDiscount>> CreateProductDiscounts(DiscountEntity discount, CreateOfferRequest request)
        {
            if (request.Products == null)
            {
                return new List<ProductDiscount>();
            }
            var productDiscounts = await _context.Products
                            .Join(request.Products, x => x.Id, x => x, (x, y) => x)
                            .Select(x => new ProductDiscount { ProductId = x.Id, DiscountId = discount.Id })
                            .ToListAsync();
            var productsDiff = request.Products.Except(productDiscounts
                                                    .Select(x => x.ProductId)
                                                    .AsEnumerable());
            if (productsDiff.Any())
            {
                throw CreateValidationException(nameof(request.Products), productsDiff);
            }

            return productDiscounts;
        }

        private async Task<List<AttributeDiscount>> CreateAttributeDiscounts(DiscountEntity discount, CreateOfferRequest request)
        {
            if (request.Attributes == null)
            {
                return new List<AttributeDiscount>();
            }
            var attributeDiscounts = await _context.Attributes
                            .Join(request.Attributes, x => x.Id, x => x, (x, y) => x)
                            .Select(x => new AttributeDiscount { AttributeId = x.Id, DiscountId = discount.Id })
                            .ToListAsync();
            var attributesDiff = request.Attributes.Except(attributeDiscounts
                                        .Select(x => x.AttributeId)
                                        .AsEnumerable());
            if (attributesDiff.Any())
            {
                throw CreateValidationException(nameof(request.Attributes), attributesDiff);
            }

            return attributeDiscounts;
        }

        private ValidationException CreateValidationException(string propertyName, IEnumerable<int> diff)
        {
            return new ValidationException(diff.Select(x => new ValidationFailure(
                    propertyName,
                    $"Obiekt o podanym Id: {x} nie istnieje"
                )).ToList());
        }

        private Task<List<DiscountModel>> GetPrescriptionDiscounts(Product product)
        {
            return _context.ProductDiscounts
                            .Where(x => x.ProductId == product.Id)
                            .GetActiveDiscounts()
                            .GetPrescriptions()
                            .Select(x => ToDiscountModelPrescription(x, product))
                            .ToListAsync();
        }

        private async Task<DiscountModel> GetOfferDiscount(Product product)
        {
            var bestOfferForProduct = await GetBestOfferForProduct(product);
            var bestOfferForAttribute = await GetBestOfferForAttribute(product);
            return bestOfferForProduct?.Price < (bestOfferForAttribute?.Price ?? decimal.MaxValue)
                   ? bestOfferForProduct : bestOfferForAttribute;
        }

        private Task<DiscountModel> GetBestOfferForAttribute(Product product)
        {
            return _context.ProductAttributesValues
                    .Where(x => x.ProductId == product.Id)
                    .SelectMany(x => x.AttributeValue.Attribute.AttributeDiscounts)
                    .Distinct()
                    .GetActiveDiscounts()
                    .BestOfferAsync(x => ToDiscountModel(x, product));
        }

        private Task<DiscountModel> GetBestOfferForProduct(Product product)
        {
            return _context.ProductDiscounts
                    .Where(x => x.ProductId == product.Id)
                    .GetActiveDiscounts()
                    .BestOfferAsync(x => ToDiscountModel(x, product));
        }

        private decimal CalculatePrice(DiscountEntity discount, decimal productPrice)
        {
            var result = productPrice;
            result -= Math.Round(productPrice * discount.Percent, 2);
            result -= discount.Value;
            return Math.Max(result, 0);
        }

        private DiscountModel ToDiscountModel(DiscountEntity discount, Product product)
        {
            var discountModel = _mapper.Map<DiscountModel>(discount);
            discountModel.Price = CalculatePrice(discount, Convert.ToDecimal(product.ProductPrice));
            return discountModel;
        }

        private DiscountModel ToDiscountModelPrescription(DiscountEntity discount, Product product)
        {
            var discountModel = ToDiscountModel(discount, product);
            discountModel.Name = discount.DiscountCategory.Name;
            return discountModel;
        }

        private bool IsOffer(DiscountEntity discount)
        {
            return discount.DiscountCategory == null;
        }

        private async Task<DiscountEntity> GetOfferByIdAsync(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                throw new NotFoundException(nameof(DiscountEntity), id);
            }

            if (!IsOffer(discount))
            {
                throw new ValidationException(new List<ValidationFailure>() {
                    new ValidationFailure("Id", $"{nameof(DiscountEntity)}({id}) is prescription discount")
                });
            }

            return discount;
        }

        private async Task<(decimal? PriceWithDiscount, int? DiscountCategoryId)> CalculatePrescriptionDiscount(Product product, int discountId)
        {
            (decimal? PriceWithDiscount, int? DiscountCategoryId) result = (null, null);
            var discount = await _context.ProductDiscounts
                                    .Where(x => x.Product == product && x.DiscountId == discountId)
                                    .Select(x => x.Discount)
                                    .FirstOrDefaultAsync();
            if(discount != null && discount.DiscountCategoryId != null)
            {
                result.DiscountCategoryId = discount.DiscountCategoryId;
                result.PriceWithDiscount = CalculatePrice(discount, (decimal)product.ProductPrice);
            }
            return result;
        }
    }

    internal static class DiscountServiceQueryExtensions
    {
        public static IQueryable<DiscountEntity> GetOffers(this IQueryable<DiscountEntity> query)
        {
            return query.Where(x => x.DiscountCategoryId == null);
        }

        public static IQueryable<DiscountEntity> GetPrescriptions(this IQueryable<DiscountEntity> query)
        {
            return query.Where(x => x.DiscountCategoryId != null)
                        .Include(x => x.DiscountCategory);
        }

        public static Task<DiscountModel> BestOfferAsync(this IQueryable<DiscountEntity> query,
                                                         Func<DiscountEntity, DiscountModel> transform)
        {
            return query.GetOffers()
                    .Select(x => transform(x))
                    .OrderBy(x => x.Price)
                    .FirstOrDefaultAsync();
        }
        public static IQueryable<DiscountEntity> GetActiveDiscounts(this IQueryable<ProductDiscount> query)
        {
            return query.Select(x => x.Discount)
                    .Where(x => x.ValidTo > DateTime.Now);
        }
        public static IQueryable<DiscountEntity> GetActiveDiscounts(this IQueryable<AttributeDiscount> query)
        {
            return query.Select(x => x.Discount)
                    .Where(x => x.ValidTo > DateTime.Now);
        }

        public static IQueryable<DiscountEntity> GetActiveDiscounts(this IQueryable<DiscountEntity> query)
        {
            return query.Where(x => x.ValidTo > DateTime.Now);
        }
    }
}
