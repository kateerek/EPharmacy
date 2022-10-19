using System.ComponentModel.DataAnnotations;
using EPharmacy.ServerApp.Models.Producer.ProducerCreation;

namespace EPharmacy.ServerApp.Models.Producer.Common
{
    public class ProducerModel : ProducerCreationRequestModel
    {
        [Required]
        public int Id { get; set; }
        
    }
}