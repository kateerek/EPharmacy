using EPharmacy.ServerApp.Models.Discounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts.Requests
{
    public class CreateOfferRequest : DiscountDetailsBaseModel
    {
        public DateTime ValidTo { get; set; }
        public IEnumerable<int> Products { get; set; }
        public IEnumerable<int> Attributes { get; set; }
    }
}
