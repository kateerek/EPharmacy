using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EPharmacy.Data.Entities.Common;
using EPharmacy.Data.Entities.Products;

namespace EPharmacy.Data.Entities.ActiveSubstances
{
    public class ActiveSubstance : BaseEntity
    {
        #region Foreign keys, relations
        public virtual List<ProductActiveSubstance> ProductActiveSubstances { get; set; }
        #endregion

        #region Public properties
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(800, MinimumLength = 1)]
        [RegularExpression(@"[^\s]+")]
        public string InternalName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        #endregion
    }
}
