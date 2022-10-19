using System.Threading.Tasks;
using EPharmacy.Data.Entities.Products;

namespace EPharmacy.ServerApp.Services.Prescription
{
    public interface IPrescriptionService
    {
        Task<PrescriptionInformation> GetPrescriptionInformationById(int prescriptionInformationId);
    }
}