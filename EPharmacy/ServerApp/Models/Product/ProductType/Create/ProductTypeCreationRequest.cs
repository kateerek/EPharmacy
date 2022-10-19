using System.ComponentModel.DataAnnotations;

namespace EPharmacy.ServerApp.Models.Product.ProductType.Create
{
    public class ProductTypeCreationRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"[^\s]+")]
        public string InternalName { get; set; }
    }
}
