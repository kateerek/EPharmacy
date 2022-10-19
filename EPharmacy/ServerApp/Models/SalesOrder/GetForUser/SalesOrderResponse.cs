using System;
using System.Collections.Generic;
using EPharmacy.ServerApp.Models.Pharmacy.Common;

namespace EPharmacy.ServerApp.Models.SalesOrder.GetForUser
{
    public class SalesOrderResponse
    {
        public int Id { get; set; }
        
        public List<ProductItemResponse> Items{ get; set; }

        public PharmacyLocationModel PharmacyLocation { get; set; }
        
        public double  TotalPrice { get; set; }

        public string Status { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}