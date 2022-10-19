using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.ActiveSubstance.GetAll
{
    public class ActiveSubstanceResponse
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string InternalName { get; set; }
    }
}
