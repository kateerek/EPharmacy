using EPharmacy.ServerApp.Models.Discounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts.Responses
{
    public class DiscountDetailsResponse
    {
        public DiscountDetailsModel Discount { get; set; }
        public IEnumerable<int> Products { get; set; }
        public IEnumerable<int> Attributes { get; set; }
    }
}
