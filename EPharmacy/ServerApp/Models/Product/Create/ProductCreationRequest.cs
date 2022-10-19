using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EPharmacy.ServerApp.Models.Discounts.Models;
using EPharmacy.ServerApp.Models.Product.Common;
using EPharmacy.ServerApp.Models.Product.Edit;

namespace EPharmacy.ServerApp.Models.Product.Create
{
    public class ProductCreationRequest
    {
        [StringLength(30)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double ProductPrice { get; set; }

        [Required]
        public ProductInformationModel ProductInformation { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        [Required]
        public int ProducerId { get; set; }

        public int? PrescriptionInformationId { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public List<ProductAttributeInformationModel> Attributes { get; set; }

        public List<PrescriptionDiscountModel> PrescriptionDiscounts { get; set; }

        [Required]
        public List<ProductActiveSubstanceModel> ProductActiveSubstances { get; set; }
    }
}
