using System.Collections.Generic;

namespace EPharmacy.ServerApp.Models.SalesOrder.Create
{
    public class SalesOrderRequest
    {

        public int PharmacyLocationId { get; set; }
        
        public List<ProductItemRequest> Items{ get; set; }
    }
}