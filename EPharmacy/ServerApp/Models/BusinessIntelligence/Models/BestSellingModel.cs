using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.BusinessIntelligence.Models
{
    public class BestSellingModel<T>
    {
        public T Model { get; set; }
        public int Count { get; set; }
    }
}
