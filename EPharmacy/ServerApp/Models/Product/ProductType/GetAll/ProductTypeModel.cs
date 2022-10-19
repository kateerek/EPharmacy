using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Product.ProductType.GetAll
{
    public class ProductTypeModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"[^\s]+")]
        public string InternalName { get; set; }
    }
}
