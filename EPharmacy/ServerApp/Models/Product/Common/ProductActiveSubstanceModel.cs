using System.ComponentModel.DataAnnotations;

namespace EPharmacy.ServerApp.Models.Product.Common
{
    public class ProductActiveSubstanceModel
    {
        [Required]
        public int ActiveSubstanceId { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
