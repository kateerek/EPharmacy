using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts.Models
{
    public class DiscountValueModel
    {
        public enum DiscountValueType
        {
            Percent,
            Value
        };

        public DiscountValueType DiscountType { get; set; }
        public decimal Value { get; set; }
    }
}
