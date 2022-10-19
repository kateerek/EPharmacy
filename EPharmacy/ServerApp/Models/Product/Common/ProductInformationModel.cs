using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Product.Common
{
    public class ProductInformationModel
    {
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
    }
}
