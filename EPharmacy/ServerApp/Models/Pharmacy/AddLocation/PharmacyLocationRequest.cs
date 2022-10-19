using System.ComponentModel.DataAnnotations;

namespace EPharmacy.ServerApp.Models.Pharmacy.AddLocation
{
    public class PharmacyLocationRequest
    {        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}