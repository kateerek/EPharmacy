using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EPharmacy.Data.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EPharmacy.Data.Entities.Products
{
    public class ProductInformation : BaseEntity
    {
        #region Foreign keys, 
        public virtual int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        #endregion

        #region Public properties
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string RecommendedIntake { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Composition { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string ImportantTips { get; set; }

        [DataType(DataType.MultilineText)]
        public string InstructionForUse { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string IndicationForUse { get; set; }
        #endregion
    }
}
