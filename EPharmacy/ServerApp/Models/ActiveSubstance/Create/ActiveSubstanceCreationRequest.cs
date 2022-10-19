using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.ActiveSubstance.Create
{
    public class ActiveSubstanceCreationRequest
    {
        #region Public properties
        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1)]
        [RegularExpression(@"[^\s]+")]
        public string InternalName { get; set; }
        #endregion
    }
}
