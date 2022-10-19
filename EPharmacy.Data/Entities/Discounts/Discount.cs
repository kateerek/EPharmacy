using EPharmacy.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPharmacy.Data.Entities.Discounts
{
    public class Discount : BaseEntity
    {
        #region Foreign keys, relations
        public virtual DiscountCategory DiscountCategory { get; set; }

        public int? DiscountCategoryId { get; set; }

        public virtual List<ProductDiscount> ProductDiscounts { get; set; }

        public virtual List<AttributeDiscount> AttributeDiscounts { get; set; }
        #endregion

        #region Public properties
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; set; }

        [Required]
        [Range(0.0, 1.0)]
        [Column(TypeName = "decimal(3, 2)")]
        public decimal Percent { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        #endregion
    }
}
