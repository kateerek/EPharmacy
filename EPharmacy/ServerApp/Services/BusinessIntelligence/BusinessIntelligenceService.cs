using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data;
using EPharmacy.Data.Entities.Products;
using EPharmacy.Data.Entities.SalesOrders;
using EPharmacy.ServerApp.Models.Attributes.GetAll;
using EPharmacy.ServerApp.Models.BusinessIntelligence.Models;
using EPharmacy.ServerApp.Models.BusinessIntelligence.Requests;
using EPharmacy.ServerApp.Models.BusinessIntelligence.Responses;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.ServerApp.Services.BusinessIntelligence
{
    using SalesOrderEntity = Data.Entities.SalesOrders.SalesOrder;
    using AttributeEntity = Data.Entities.Attributes.Attribute;
    using EPharmacy.Data.Entities.Attributes;
    using EPharmacy.ServerApp.Models.Discounts.Models;
    using EPharmacy.ServerApp.Models.Product.Common;

    public class BusinessIntelligenceService : IBusinessIntelligenceService
    {
        private readonly EPharmacyContext _context;
        private readonly IMapper _mapper;

        public BusinessIntelligenceService(EPharmacyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BestSellingResponse<ProductShortModel>> GetBestSellingProducts(BestSellingRequest bestSellingRequest)
        {
            return await GetBestSelling(x => x.Product,
                                        x => _mapper.Map<ProductShortModel>(x),
                                        bestSellingRequest);
        }

        public async Task<BestSellingResponse<AttributeResponseModel>> GetBestSellingAttributes(BestSellingRequest bestSellingRequest)
        {
            return await PerformOnSalesOrdersAsync(async (req, query) =>
            {
                var attributesItems = await query.SelectMany(x => x.Items)
                                   .Select(x => x.Product.AttributesValues
                                                .Select(y => new Tuple<AttributeEntity, int>(y.AttributeValue.Attribute, x.ItemCount))
                                                .Distinct()
                                                .ToList())
                                   .ToListAsync();

                var dict = FlatAttributesItems(attributesItems);

                return ProjectToBestSellingModelList(dict,
                    kv => new BestSellingModel<AttributeResponseModel>()
                    {
                        Model = _mapper.Map<AttributeResponseModel>(kv.Key),
                        Count = kv.Value
                    },
                    req.Limit ?? int.MaxValue);
            }, bestSellingRequest);
        }

        public async Task<BestSellingResponse<DiscountInfoModel>> GetBestSellingOfferDiscounts(BestSellingRequest bestSellingRequest)
        {
            return await GetBestSelling(x => x.DiscountCategory == null && x.Discount != null,
                                        x => x.Discount,
                                        x => _mapper.Map<DiscountInfoModel>(x),
                                        bestSellingRequest);
        }

        public async Task<BestSellingResponse<PrescriptionCategoryInfoModel>> GetBestSellingPrescriptionDiscounts(BestSellingRequest bestSellingRequest)
        {
            return await GetBestSelling(x => x.DiscountCategory != null && x.Discount != null,
                                        x => x.DiscountCategory,
                                        x => _mapper.Map<PrescriptionCategoryInfoModel>(x),
                                        bestSellingRequest);
        }

        public async Task<BestSellingResponse<PharmacyLocation>> GetBestSellingByPharmacyLocation(BestSellingRequest bestSellingRequest)
        {
            return await GetBestSelling(x => x.SalesOrder.PharmacyLocation,
                                        x => x,
                                        bestSellingRequest);
        }

        private Dictionary<AttributeEntity, int> FlatAttributesItems(List<List<Tuple<AttributeEntity, int>>> attributesItems)
        {
            var dict = new Dictionary<AttributeEntity, int>();
            foreach (var elem in attributesItems)
            {
                foreach (var attributeItem in elem)
                {
                    var id = attributeItem.Item1;
                    var val = attributeItem.Item2;
                    if (dict.ContainsKey(id))
                    {
                        dict[id] += val;
                    }
                    else
                    {
                        dict.Add(id, val);
                    }
                }
            }
            return dict;
        }

        private IQueryable<SalesOrderEntity> GetSalesOrdersQueryable(DateTime from, DateTime to)
        {
            return _context.SalesOrders.Where(x => from <= x.OrderDate && x.OrderDate <= to);
        }

        private async Task<BestSellingResponse<T>> PerformOnSalesOrdersAsync<T>(
                    Func<BestSellingRequest, IQueryable<SalesOrderEntity>, Task<IList<BestSellingModel<T>>>> getBestSellingModels,
                    BestSellingRequest bestSellingRequest)
        {
            var query = GetSalesOrdersQueryable(bestSellingRequest.From, bestSellingRequest.To);
            return new BestSellingResponse<T> { Values = await getBestSellingModels(bestSellingRequest, query) };
        }

        private IList<BestSellingModel<T>> ProjectToBestSellingModelList<V, T>(
            IEnumerable<V> enumerable,
            Func<V, BestSellingModel<T>> mappingFunction,
            int limit)
        {
            return enumerable.Select(x => mappingFunction(x))
                        .OrderByDescending(x => x.Count)
                        .Take(limit)
                        .ToList();
        }

        private async Task<IList<BestSellingModel<T>>> ProjectToBestSellingModelList<E, T>(
                    IQueryable<IGrouping<E, ProductItem>> query,
                    Func<E, T> mappingFunction,
                    int limit)
        {
            // Workaround for 
            // https://github.com/aspnet/EntityFrameworkCore/issues/12560
            var tmp = await query.ToListAsync();
            return tmp.Select(x => new BestSellingModel<T>()
                        {
                            Model = mappingFunction(x.Key),
                            Count = x.Select(y => y.ItemCount).Sum()
                        })
                        .OrderByDescending(x => x.Count)
                        .Take(limit)
                        .ToList();
        }

        private async Task<BestSellingResponse<T>> GetBestSelling<E, T>(
                Expression<Func<ProductItem, bool>> whereExpression,
                Expression<Func<ProductItem, E>> groupByExpression,
                Func<E, T> mappingFunction,
                BestSellingRequest bestSellingRequest)
        {
            return await PerformOnSalesOrdersAsync(async (req, query) =>
            {
                return await ProjectToBestSellingModelList(
                             query.SelectMany(x => x.Items)
                                  .Where(whereExpression)
                                  .GroupBy(groupByExpression)
                             , mappingFunction
                             , req.Limit ?? int.MaxValue);
            }, bestSellingRequest);
        }

        private async Task<BestSellingResponse<T>> GetBestSelling<E, T>(
                Expression<Func<ProductItem, E>> groupByExpression,
                Func<E, T> mappingFunction,
                BestSellingRequest bestSellingRequest)
        {
            return await PerformOnSalesOrdersAsync(async (req, query) =>
            {
                return await ProjectToBestSellingModelList(
                             query.SelectMany(x => x.Items)
                                  .GroupBy(groupByExpression)
                             , mappingFunction
                             , req.Limit ?? int.MaxValue);
            }, bestSellingRequest);
        }
    }
}
