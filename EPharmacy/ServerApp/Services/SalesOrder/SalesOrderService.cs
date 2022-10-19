using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data;
using EPharmacy.Data.Constants;
using EPharmacy.ServerApp.Models.SalesOrder.Create;
using EPharmacy.ServerApp.Models.SalesOrder.GetForUser;
using EPharmacy.ServerApp.Services.Discount;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.ServerApp.Services.SalesOrder
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly EPharmacyContext _context;
        private readonly IMapper _mapper;
        private readonly IDiscountService _discountService;

        public SalesOrderService(EPharmacyContext context, IMapper mapper, IDiscountService discountService)
        {
            _context = context;
            _mapper = mapper;
            _discountService = discountService;
        }

        public async Task<IList<SalesOrderResponse>> GetSalesOrdersForUserId(string userId)
        {
            var salesOrderList = await _context.SalesOrders.Where(x => x.ApplicationUserId == userId).ToListAsync();
            var mappedList = _mapper.Map<List<SalesOrderResponse>>(salesOrderList);
            foreach (var salesOrderResponse in mappedList)
            {
                salesOrderResponse.TotalPrice = CountOverallPrice(salesOrderResponse);
            }

            return mappedList;
        }

        public async Task<bool> CreateSalesOrder(Data.Entities.SalesOrders.SalesOrder salesOrderEntity, string userId)
        {

            salesOrderEntity.ApplicationUserId = userId;
            _context.SalesOrders.Add(salesOrderEntity);
            return await _context.SaveChangesAsync() > 0;
        }        

        public async Task<IList<SalesOrderResponse>> GetInProgress()
        {
            var salesOrderList =  _mapper.Map<List<SalesOrderResponse>>(await _context.SalesOrders
                .Where(x => x.Status == SalesOrderStatuses.InProgress).ToListAsync());
            salesOrderList.ForEach(x => x.TotalPrice = CountOverallPrice(x));
            return salesOrderList;
        }

        public Data.Entities.SalesOrders.SalesOrder Map(SalesOrderRequest request)
        {
            return _mapper.Map<Data.Entities.SalesOrders.SalesOrder>(request);
        }

        public async Task<IList<SalesOrderResponse>> GetCompleted()
        {
            var salesOrderList =  _mapper.Map<List<SalesOrderResponse>>(await _context.SalesOrders
                .Where(x => x.Status == SalesOrderStatuses.Completed).ToListAsync());
            salesOrderList.ForEach(x => x.TotalPrice = CountOverallPrice(x));
            return salesOrderList;
        }

        public async Task<bool> SetAsCompleted(int id)
        {
            var entity = await _context.SalesOrders.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null || entity.Status == SalesOrderStatuses.Completed) return false;
            entity.Status = SalesOrderStatuses.Completed;
            entity.EndDate = DateTime.UtcNow;
            _context.SalesOrders.Update(entity);
            return await _context.SaveChangesAsync() > 0;

        }

        private static double CountOverallPrice(SalesOrderResponse salesOrderResponse)
        {
            
            return salesOrderResponse.Items.Sum(x => (x.PriceWithDiscount ?? x.Product.ProductPrice) * x.ItemCount);
        }
    }
}