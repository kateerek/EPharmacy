using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EPharmacy.Data.Entities.Common;
using EPharmacy.Data.Entities.Products;

namespace EPharmacy.Data.Entities.Attributes
{
    public class AttributeValue : BaseEntity
    {
        #region Foreign keys, relations
        public virtual Attribute Attribute { get; set; }
        public virtual List<ProductAttributeValue> ProductsAttributeValues { get; set; }
        #endregion

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Value { get; set; }
    }
}
 