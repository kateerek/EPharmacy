using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.BusinessIntelligence.Requests
{
    public class BestSellingRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int? Limit { get; set; }
    }
}
