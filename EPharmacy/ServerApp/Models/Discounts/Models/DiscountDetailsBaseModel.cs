using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts.Models
{
    public class DiscountDetailsBaseModel
    {
        public DiscountValueModel DiscountValue { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
