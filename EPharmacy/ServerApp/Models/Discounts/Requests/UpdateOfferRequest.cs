using EPharmacy.ServerApp.Models.Discounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts.Requests
{
    public class UpdateOfferRequest : CreateOfferRequest
    {
        public int Id { get; set; }
    }
}
