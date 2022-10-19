using System;
using EPharmacy.Data.Entities.Common;
using EPharmacy.Data.Entities.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPharmacy.Data.Entities.SalesOrders
{
    public class SalesOrder : BaseEntity
    {
        [Required]
        public string ApplicationUserId { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int PharmacyLocationId { get; set; }
        
        public virtual PharmacyLocation PharmacyLocation { get; set; }

        public virtual List<ProductItem> Items { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }
        
    }
}