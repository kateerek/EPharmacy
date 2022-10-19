using EPharmacy.Data.Entities.Common;
using EPharmacy.Data.Entities.Discounts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPharmacy.Data.Entities.Attributes
{
    public class Attribute : BaseEntity
    {
        #region Foreign keys, relations
        ///TODO       
        //[NotMapped] //Temporary solution for only "true" and "false" solution of attribute values.
        public virtual List<AttributeValue> AttributeValues { get; set; }

        public virtual List<AttributeDiscount> AttributeDiscounts { get; set; }
        #endregion

        #region Public properties
        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1)]
        [RegularExpression(@"[^\s]+")]
        public string InternalName { get; set; }

        public string Description { get; set; }
        #endregion
    }
}
