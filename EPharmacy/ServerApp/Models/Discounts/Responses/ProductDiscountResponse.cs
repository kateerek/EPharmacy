using EPharmacy.ServerApp.Models.Discounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts.Responses
{
    public class ProductDiscountResponse
    {
        public IList<DiscountModel> PrescriptionDiscounts { get; set; }
        public DiscountModel OfferDiscount { get; set; }
    }
}
