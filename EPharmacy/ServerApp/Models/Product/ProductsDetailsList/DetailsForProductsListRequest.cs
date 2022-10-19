using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Product.ProductsDetailsList
{
    public class DetailsForProductsListRequest
    {
        public IEnumerable<int> ProductIds { get; set; }
    }
}
