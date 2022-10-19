using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EPharmacy.Data.Entities.Common;
using EPharmacy.Data.Entities.Discounts;

namespace EPharmacy.Data.Entities.Products
{
    public class Product : BaseEntity
    {
        #region Foreign keys, relations
        [Required]
        public virtual ProductType  ProductType { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
        [Required]
        public virtual ProductInformation ProductInformation { get; set; }
        [Required]
        public virtual Producer Producer { get; set; }
        [Required]
        public int ProducerId { get; set; }
        public virtual List<ProductAttributeValue> AttributesValues { get; set; }            
        public virtual PrescriptionInformation PrescriptionInformation { get; set; }
        public virtual List<ProductDiscount> ProductDiscounts { get; set; }
        public virtual List<FavouriteProduct> FavouriteProducts { get; set; }
        public virtual List<ProductActiveSubstance> ProductActiveSubstances { get; set; }
        #endregion

        #region Public properties
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double ProductPrice { get; set; }

        public string ImageUrl { get; set; }
        
        #endregion

    }
}
