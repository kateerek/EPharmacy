using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EPharmacy.ServerApp.Models.Discounts.Responses;
using EPharmacy.ServerApp.Models.Producer.Common;
using EPharmacy.ServerApp.Models.Product.Common;
using EPharmacy.ServerApp.Models.Product.ProductType.GetAll;

namespace EPharmacy.ServerApp.Models.Product.Details
{
    public class ProductDetailsModel
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double ProductPrice { get; set; }

        public bool? IsFavourite { get; set; } = null;

        [Required]
        public ProductInformationModel ProductInformation { get; set; }

        [Required]
        public ProductTypeModel ProductType { get; set; }

        [Required]
        public ProducerModel Producer { get; set; }

        public PrescriptionModel PrescriptionInformation { get; set; }

        public string ImageUrl { get; set; }
        public ProductDiscountResponse ProductDiscounts { get; set; }
    }
}
