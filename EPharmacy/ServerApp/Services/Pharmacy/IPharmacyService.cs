using System.Collections.Generic;
using System.Threading.Tasks;
using EPharmacy.ServerApp.Models.Pharmacy.AddLocation;
using EPharmacy.ServerApp.Models.Pharmacy.Common;

namespace EPharmacy.ServerApp.Services.Pharmacy
{
    public interface IPharmacyService
    {
        Task<IList<PharmacyLocationModel>> GetAllPharmacies();
        Task AddPharmacyLocation(PharmacyLocationRequest pharmacyLocationRequest);
        Task<bool> EditPharmacyLocation(PharmacyLocationModel pharmacyLocationModel);
        Task<bool> RemovePharmacyLocation(int id);
    }
}