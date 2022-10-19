using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Producer.ProducerCreation
{
    public class ProducerCreationRequestModel
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
