using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts.Models
{
    public class PrescriptionDiscountModel
    {
        public int PrescriptionCategoryId { get; set; }
        public DiscountValueModel DiscountValue { get; set; }
    }
}
