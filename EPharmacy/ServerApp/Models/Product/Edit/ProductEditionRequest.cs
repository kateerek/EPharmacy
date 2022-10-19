using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EPharmacy.ServerApp.Models.Product.Common;
using EPharmacy.ServerApp.Models.Product.Create;

namespace EPharmacy.ServerApp.Models.Product.Edit
{
    public class ProductEditionRequest : ProductCreationRequest
    {
        [Required]
        public int Id { get; set; }

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

        [Required]
        public int? PrescriptionInformationId { get; set; }

        [Required]
        public List<ProductAttributeInformationModel> Attributes { get; set; }

        [Required]
        public List<ProductActiveSubstanceModel> ProductActiveSubstances { get; set; }
    }
}
