using EPharmacy.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace EPharmacy.Data.Entities.SalesOrders
{
    public class PharmacyLocation : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}