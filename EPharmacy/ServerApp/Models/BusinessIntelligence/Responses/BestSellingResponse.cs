using EPharmacy.ServerApp.Models.BusinessIntelligence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.BusinessIntelligence.Responses
{
    public class BestSellingResponse<T>
    {
        public IList<BestSellingModel<T>> Values { get; set; }
    }
}
