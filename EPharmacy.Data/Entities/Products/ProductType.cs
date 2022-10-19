using EPharmacy.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;

namespace EPharmacy.Data.Entities.Products
{
    public class ProductType : BaseEntity
    {
        #region Foreign keys, relations
        public virtual List<Product> Products { get; set; }
        #endregion

        #region Public properties
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"[^\s]+")]
        public string InternalName { get; set; }
        #endregion
    }
}
