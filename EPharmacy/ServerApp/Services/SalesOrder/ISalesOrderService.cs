using System.Collections.Generic;
using System.Threading.Tasks;
using EPharmacy.Data.Constants;
using EPharmacy.ServerApp.Controllers;
using EPharmacy.ServerApp.Models.SalesOrder.Create;
using EPharmacy.ServerApp.Models.SalesOrder.GetForUser;

namespace EPharmacy.ServerApp.Services.SalesOrder
{
    public interface ISalesOrderService
    {
        Task<IList<SalesOrderResponse>> GetSalesOrdersForUserId(string userId);
        Task<bool> CreateSalesOrder(Data.Entities.SalesOrders.SalesOrder salesOrder, string userId);        
        Task<IList<SalesOrderResponse>> GetInProgress();

        Data.Entities.SalesOrders.SalesOrder Map(SalesOrderRequest request);


        Task<IList<SalesOrderResponse>> GetCompleted();
        Task<bool> SetAsCompleted(int id);
    }
}