using System.ComponentModel.DataAnnotations;
using EPharmacy.ServerApp.Models.Pharmacy.AddLocation;

namespace EPharmacy.ServerApp.Models.Pharmacy.Common
{
    public class PharmacyLocationModel : PharmacyLocationRequest
    {
        [Required]
        public int Id { get; set; }        
    }
}